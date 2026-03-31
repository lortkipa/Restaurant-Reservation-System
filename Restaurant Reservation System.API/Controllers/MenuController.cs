using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Menu;
using Restaurant_Reservation_System.Service.Interfaces;

namespace Restaurant_Reservation_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMenuRepository _menuRepository;
        private readonly RestaurantContext _context;

        public MenuController(IMenuService menuService, IMenuRepository menuRepository, RestaurantContext context)
        {
            _menuService = menuService;
            _menuRepository = menuRepository;
            _context = context;
        }

        // Admin only
        [HttpPost("Add")]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuDTO model)
        {
            var menu = await _menuService.CreateAsync(model);
            return Ok(menu);
        }

        // Admin + Customer
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _menuService.GetAllAsync();
            return Ok(menus);
        }

        // Admin + Customer
        [HttpGet("GetDishById/{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            var dish = await _context.Set<MenuItem>()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dish == null)
                return NotFound("Dish not found.");

            return Ok(new MenuItemDTO
            {
                Name = dish.Name,
                Price = dish.Price,
                IsAvaiable = dish.IsAvaiable
            });
        }

        // Admin + Customer
        [HttpGet("GetAllWithDishes")]
        public async Task<IActionResult> GetMenusWithDishes()
        {
            var menus = await _context.Set<Menu>()
                .Include(m => m.MenuItems)
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.RestaurantId,
                    Dishes = m.MenuItems
                        .Select(d => new { d.Id, d.Name, d.Price, d.IsAvaiable })
                })
                .ToListAsync();

            return Ok(menus);
        }
        [HttpGet("GetMenusWithDishes/{restaurantId}")]
        public async Task<IActionResult> GetMenusWithDishesByRestaurant(int restaurantId)
        {
            var menus = await _context.Set<Menu>()
                .Where(m => m.RestaurantId == restaurantId)
                .Include(m => m.MenuItems)
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.RestaurantId,
                    Dishes = m.MenuItems
                        .Select(d => new { d.Id, d.Name, d.Price, d.IsAvaiable })
                })
                .ToListAsync();

            if (!menus.Any())
                return NotFound($"No menus found for restaurant ID {restaurantId}");

            return Ok(menus);
        }
        [HttpGet("GetWithAvailableDishes")]
        public async Task<IActionResult> GetMenusWithAvailableDishes()
        {
            var menus = await _context.Set<Menu>()
                .Include(m => m.MenuItems)
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.RestaurantId,
                    AvailableDishes = m.MenuItems
                        .Where(d => d.IsAvaiable)
                        .Select(d => new { d.Id, d.Name, d.Price })
                })
                .ToListAsync();

            return Ok(menus);
        }

        // Admin only
        [HttpPost("AddDish/{id}")]
        public async Task<IActionResult> AddDishToMenu(int id, [FromBody] MenuItemDTO item)
        {
            var menu = await _context.Set<Menu>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menu == null)
                return NotFound("Menu not found.");

            var dish = new MenuItem
            {
                MenuId = id,
                Name = item.Name,
                Price = item.Price,
                IsAvaiable = item.IsAvaiable
            };

            _context.Set<MenuItem>().Add(dish);
            await _context.SaveChangesAsync();

            return Ok("Dish added successfully.");
        }

        // Admin only
        [HttpPut("UpdateDish/{menuItemId}")]
        public async Task<IActionResult> UpdateDish(int menuItemId, [FromBody] MenuItemDTO item)
        {
            var dish = await _context.Set<MenuItem>()
                .FirstOrDefaultAsync(d => d.Id == menuItemId);

            if (dish == null)
                return NotFound("Dish not found.");

            dish.Name = item.Name;
            dish.Price = item.Price;
            dish.IsAvaiable = item.IsAvaiable;

            await _context.SaveChangesAsync();
            return Ok("Dish updated successfully.");
        }

        // Admin only
        [HttpPatch("SetAvailability/{menuItemId}")]
        public async Task<IActionResult> SetAvailability(int menuItemId, [FromBody] bool isAvailable)
        {
            var dish = await _context.Set<MenuItem>()
                .FirstOrDefaultAsync(d => d.Id == menuItemId);

            if (dish == null)
                return NotFound("Dish not found.");

            dish.IsAvaiable = isAvailable;
            await _context.SaveChangesAsync();

            return Ok("Availability updated.");
        }

        // Admin only
        [HttpDelete("Remove/{menuId}/dishes/{dishId}")]
        public async Task<IActionResult> RemoveDishFromMenu(int menuId, int dishId)
        {
            var dish = await _context.Set<MenuItem>()
                .FirstOrDefaultAsync(d => d.Id == dishId && d.MenuId == menuId);

            if (dish == null)
                return NotFound("Dish not found in this menu.");

            _context.Set<MenuItem>().Remove(dish);
            await _context.SaveChangesAsync();

            // Return 204 No Content since deletion was successful
            return NoContent();
        }
    }
}