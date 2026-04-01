using AutoMapper;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Menu;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.Restaurant;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repo;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllAsync()
        {
            var restaurants = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        }
        public async Task<RestaurantDTO> GetByIdAsync(int id)
        {
            var restaurant = await _repo.GetByIdAsync(id);
            return restaurant == null ? null : _mapper.Map<RestaurantDTO>(restaurant);
        }
        public async Task<RestaurantDTO> AddAsync(CreateRestaurantDTO model)
        {
            Restaurant restaurant = _mapper.Map<Restaurant>(model);

            await _repo.AddAsync(restaurant);
            return _mapper.Map<RestaurantDTO>(restaurant);
        }
        public async Task<bool> UpdateAsync(int id, UpdateRestaurantDTO model)
        {
            var restaurant = await _repo.GetByIdAsync(id);
            if (restaurant == null)
                return false;

            // Map only the fields from model to the existing restaurant
            if (!string.IsNullOrEmpty(model.Name))
                restaurant.Name = model.Name;

            if (!string.IsNullOrEmpty(model.Location))
                restaurant.Location = model.Location;

            if (!string.IsNullOrEmpty(model.Description))
                restaurant.Description = model.Description;

            if (!string.IsNullOrEmpty(model.Email))
                restaurant.Email = model.Email;

            if (model.TotalTables > 0)
                restaurant.TotalTables = model.TotalTables;

            if (model.SeatsPerTable > 0)
                restaurant.SeatsPerTable = model.SeatsPerTable;

            // Save changes
            await _repo.UpdateAsync(restaurant);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var restaurant = await _repo.GetByIdAsync(id);
            if (restaurant == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
