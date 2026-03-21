using AutoMapper;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.RoleUser;
using Restaurant_Reservation_System.Service.DTOs.User;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class RoleUserService : IRoleUserService
    {
        private readonly IRoleUserRepository _repo;
        private readonly IMapper _mapper;

        public RoleUserService(IRoleUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RoleUserDTO> GetByIdAsync(int id)
        {
            RoleUser roleUser = await _repo.GetByIdAsync(id);
            if (roleUser == null) throw new Exception("user not found");

            return _mapper.Map<RoleUserDTO>(roleUser);
        }
        public async Task<IEnumerable<RoleDTO>> GetRolesByUserId(int userId)
        {
            var roles = await _repo.GetRolesByUserId(userId);

            if (roles == null) new Exception("user has no roles");

            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
        public async Task<bool> SetRoleByUserId(int userId, int roleId)
        {
            var existingRoles = await _repo.GetRolesByUserId(userId);
            if (existingRoles.Any(r => r.Id == roleId)) return false; 

            var roleUser = new RoleUser
            {
                UserId = userId,
                RoleId = roleId
            };

            await _repo.AddAsync(roleUser);
            return true;
        }
        public async Task<bool> RemoveRoleByUserId(int userId, int roleId)
        {
            var roles = await _repo.GetRolesByUserId(userId);
            var roleToRemove = roles.FirstOrDefault(r => r.Id == roleId);

            if (roleToRemove == null) return false; 

            await _repo.DeleteByUserAndRoleId(userId, roleId);
            return true;
        }
        public async Task<RoleUserDTO> CreateAsync(CreateRoleUserDTO model)
        {
            if (model == null) return null;

            RoleUser roleUser = _mapper.Map<RoleUser>(model);

            await _repo.AddAsync(roleUser);
            return _mapper.Map<RoleUserDTO>(roleUser);
        }
        public async Task<bool> UpdateAsync(int id, CreateRoleUserDTO model)
        {
            RoleUser roleUser = await _repo.GetByIdAsync(id);
            if (roleUser == null) return false;

            _mapper.Map(model, roleUser);
            await _repo.UpdateAsync(roleUser);
            return true;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            RoleUser roleUser = await _repo.GetByIdAsync(id);
            if (roleUser == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
