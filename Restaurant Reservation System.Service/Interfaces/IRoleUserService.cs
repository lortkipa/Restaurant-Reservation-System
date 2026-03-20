using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.RoleUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IRoleUserService
    {
        Task<RoleUserDTO> GetByIdAsync(int id);
        Task<IEnumerable<RoleDTO>> GetRolesByUserId(int userId);
        Task<RoleUserDTO> CreateAsync(CreateRoleUserDTO model);
        Task<bool> SetRoleByUserId(int userId, int roleId);
        Task<bool> RemoveRoleByUserId(int userId, int roleId);
        Task<bool> UpdateAsync(int id, CreateRoleUserDTO model);
        Task<bool> RemoveAsync(int id);
    }
}
