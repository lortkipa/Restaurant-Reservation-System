using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.RoleUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IRoleUserService
    {
        Task<RoleUserDTO> GetByIdAsync(int id);
        Task<RoleUserDTO> CreateAsync(CreateRoleUserDTO model);
        Task<bool> UpdateAsync(int id, CreateRoleUserDTO model);
        Task<bool> RemoveAsync(int id);
    }
}
