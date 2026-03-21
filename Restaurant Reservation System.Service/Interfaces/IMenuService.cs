using Restaurant_Reservation_System.Service.DTOs.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDTO>> GetAllAsync();
        Task<MenuDTO> GetByIdAsync(int id);
        Task<MenuDTO> CreateAsync(CreateMenuDTO model);
        Task<bool> UpdateAsync(int id, UpdateMenuDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
