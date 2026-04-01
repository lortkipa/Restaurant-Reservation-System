using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Restaurant;
using Restaurant_Reservation_System.Service.Interfaces;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        // 🔹 PUBLIC
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll()
        {
            var restaurants = await _service.GetAllAsync();
            return Ok(restaurants);
        }

        // 🔹 PUBLIC
        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<RestaurantDTO>> GetById(int id)
        {
            var restaurant = await _service.GetByIdAsync(id);

            if (restaurant == null)
                return NotFound("Restaurant not found");

            return Ok(restaurant);
        }

        // 🔹 ADMIN ONLY
        //[Authorize(Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<ActionResult<AuthResponseDTO>> Add(CreateRestaurantDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Invalid data"
                });

            var restaurant = await _service.AddAsync(model);

            if (restaurant == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Restaurant creation failed"
                });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Restaurant added successfully"
            });
        }

        // 🔹 ADMIN ONLY
        //[Authorize]
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> Update(int id, UpdateRestaurantDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Invalid data"
                });

            var restaurant = await _service.GetByIdAsync(id);

            if (restaurant == null)
                return NotFound(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Restaurant not found"
                });

            var result = await _service.UpdateAsync(id, model);

            if (!result)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Restaurant update failed"
                });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Restaurant updated successfully"
            });
        }

        // 🔹 ADMIN ONLY
        //[Authorize]
        [HttpDelete("Remove/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> Delete(int id)
        {
            var restaurant = await _service.GetByIdAsync(id);

            if (restaurant == null)
                return NotFound(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Restaurant not found"
                });

            bool res = await _service.DeleteAsync(id);

            if (!res)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Delete failed"
                });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Restaurant deleted successfully"
            });
        }
    }
}