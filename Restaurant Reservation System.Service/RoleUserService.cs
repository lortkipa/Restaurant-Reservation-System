using AutoMapper;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Person;
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

            roleUser = _mapper.Map<RoleUser>(model);
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
