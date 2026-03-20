using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs;
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
        private readonly IRoleUserRepository _roleUserRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IPersonRepository personRepo, IRoleUserRepository roleUserRepo, IPasswordHasher passwordHaser, IMapper mapper)
        {
            _userRepo = userRepo;
            _personRepo = personRepo;
            _roleUserRepo = roleUserRepo;
            _passwordHasher = passwordHaser;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserDTO>(user);
        }
        public async Task<RoleDTO> GetRoleById(int id)
        {
            Role role = await _userRepo.GetRoleById(id);
            if (role == null) throw new Exception("role not found");
            return _mapper.Map<RoleDTO>(role);
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

            RoleUser roleUser = new RoleUser
            {
                UserId = user.Id,
                RoleId = 3 // Costumer by default
            };
            await _roleUserRepo.AddAsync(roleUser);

            return new AuthResponseDTO { Status = true, Message = "Registration successful" };
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginUserDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Password))
                return new AuthResponseDTO { Status = false, Message = "Invalid object or password" };

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
