using Restaurant_Reservation_System.Service.DTOs.Restaurant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllAsync();
        Task<RestaurantDTO> GetByIdAsync(int id);
        Task<RestaurantDTO> AddAsync(CreateRestaurantDTO model);
        Task<bool> UpdateAsync(int id, UpdateRestaurantDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
