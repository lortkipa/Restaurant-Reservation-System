using AutoMapper;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Menu;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDTO>> GetAllAsync()
        {
            var menus = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuDTO>>(menus);
        }
        public async Task<MenuDTO> GetByIdAsync(int id)
        {
            var menu = await _repo.GetByIdAsync(id);
            if (menu == null) throw new Exception("menu not found");

            return _mapper.Map<MenuDTO>(menu);
        }
        public async Task<MenuDTO> CreateAsync(CreateMenuDTO model)
        {
            Menu menu = _mapper.Map<Menu>(model);
            await _repo.AddAsync(menu);

            return _mapper.Map<MenuDTO>(menu);
        }
        public async Task<bool> UpdateAsync(int id, UpdateMenuDTO model)
        {
            Menu menu = await _repo.GetByIdAsync(id);
            if (menu == null) return false;

            _mapper.Map(model, menu);
            await _repo.UpdateAsync(menu);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var menu = await _repo.GetByIdAsync(id);
            if (menu == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
