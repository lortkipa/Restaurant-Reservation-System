using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.Reservation;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.User;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IReservationRepository _reservationRepo;
        private readonly IRoleUserRepository _roleUserRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IEmailJSRepository _emailJSRepo;

        public UserService(IEmailJSRepository emailJSRepository, IUserRepository userRepo, IPersonRepository personRepo, IReservationRepository reservationRepo, IRoleUserRepository roleUserRepo, IPasswordHasher passwordHaser, IMapper mapper)
        {
            _emailJSRepo = emailJSRepository;
            _userRepo = userRepo;
            _personRepo = personRepo;
            _roleUserRepo = roleUserRepo;
            _reservationRepo = reservationRepo;
            _passwordHasher = passwordHaser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<IEnumerable<UserDTO>> GetAllAdminsAsync()
        {
            IEnumerable<User> _admins = await _userRepo.GetAllWorkersAsync();
            IEnumerable<UserDTO> admins = _mapper.Map<IEnumerable<UserDTO>>(_admins);

            return admins;
        }
        public async Task<IEnumerable<UserDTO>> GetAllWorkersAsync()
        {
            IEnumerable<User> _workers = await _userRepo.GetAllWorkersAsync();
            IEnumerable<UserDTO> workers = _mapper.Map<IEnumerable<UserDTO>>(_workers);

            return workers;
        }
        public async Task<IEnumerable<CustomerDTO>> GetAllCostumersAsync()
        {
            var userCustomers = await _userRepo.GetAllCostumersAsync();

            var customers = _mapper.Map<IEnumerable<CustomerDTO>>(userCustomers);

            foreach (var customer in customers)
            {
                var reservations = await _reservationRepo.GetByCustomerIdAsync(customer.Id);

                customer.Reservations = _mapper.Map<List<ReservationDTO>>(reservations);
            }

            return customers;
        }
        public async Task<UserPersonDTO> GetByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            var person = await _personRepo.GetByIdAsync(user.PersonId);
            if (person == null) { return null;  }

            return new UserPersonDTO
            {
                user = _mapper.Map<UserDTO>(user),
                person = _mapper.Map<PersonDTO>(person)
            };
        }
        public async Task<IEnumerable<RoleDTO>> GetRolesById(int id)
        {
            var roles = await _roleUserRepo.GetRolesByUserId(id);
            if (!roles.Any()) return null;
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) throw new Exception("user not found");
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetByUsernameAsync(string username)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null) throw new Exception("user not found");
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<AuthResponseDTO> RegisterAsync(RegisterUserDTO model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrEmpty(model.Password))
                return new AuthResponseDTO { Status = false, Message = "Invalid object, email or password" };
            if (model.Person == null)
                return new AuthResponseDTO { Status = false, Message = "Person data required" };

            var existingUser = await _userRepo.GetByEmailAsync(model.Email);
            if (existingUser != null)
                return new AuthResponseDTO { Status = false, Message = "Email already in use" };

            Person person = _mapper.Map<Person>(model.Person);
            await _personRepo.AddAsync(person);

            User user = _mapper.Map<User>(model);
            user.PersonId = person.Id;
            user.PasswordHash = await _passwordHasher.HashPassword(model.Password);
            await _userRepo.AddAsync(user);

            await _emailJSRepo.AddAsync(new EmailJS
            {
                UserId = user.Id,
                ServiceId = null,
                TemplateId = null,
                PublicKey = null
            });

            RoleUser roleUser = new RoleUser
            {
                UserId = user.Id,
                RoleId = 3 // Costumer by default
            };
            await _roleUserRepo.AddAsync(roleUser);

            return new AuthResponseDTO { Status = true, Message = "Registration was successful" };
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginUserDTO model)
        {
            var user = await _userRepo.GetByUsernameAsync(model.Username);
            if (user == null)
                return new AuthResponseDTO { Status = false, Message = "User doesn't exist" };

            if (await _passwordHasher.VerifyPassword(model.Password, user.PasswordHash) == false)
                return new AuthResponseDTO { Status = false, Message = "Invalid password" };

            return new AuthResponseDTO { Status = true, Message = "login was successful" };
        }
        public async Task<bool> UpdateAsync(int id, UpdateUserDTO model)
        {
            if (model == null)
                return false;

            User user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return false;

            user = _mapper.Map(model, user);
            user.PasswordHash = await _passwordHasher.HashPassword(model.Password);

            await _userRepo.UpdateAsync(user);
            return true;
        }
        public async Task<bool> UpdateWithoutPasswordAsync(int id, UpdateUserDTO model)
        {
            if (model == null)
                return false;

            User user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return false;
            string passwordHash = user.PasswordHash;

            user = _mapper.Map(model, user);
            user.PasswordHash = passwordHash;

            // user.PasswordHash = await _passwordHasher.HashPassword(model.Password);

            await _userRepo.UpdateAsync(user);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            await _personRepo.DeleteAsync(user.PersonId);
            await _userRepo.DeleteAsync(id);
            return true;
        }
    }
}
