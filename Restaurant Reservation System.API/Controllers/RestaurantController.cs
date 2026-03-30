using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Restaurant;
using Restaurant_Reservation_System.Service.DTOs.User;
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll()
        {
            var restaurants = await _service.GetAllAsync();
            return Ok(restaurants);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<RestaurantDTO>> GetUserById(int id)
        {
            var restaurants = await _service.GetByIdAsync(id);
            if (restaurants == null)
                return NotFound("User not found");

            return Ok(restaurants);
        }
        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(CreateRestaurantDTO model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = await _service.AddAsync(model);
                if (restaurant == null) return NotFound(new AuthResponseDTO
                                        {
                                            Status = false,
                                            Message = "Restaurant Creation Failed"
                                        });
                return Ok(new AuthResponseDTO
                {
                    Status = true,
                    Message = "Restaurant Added Successfully"
                });
            }

            return BadRequest(new AuthResponseDTO
            {
                Status = false,
                Message = "Restaurant Creation Failed"
            });
        }
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> Update(int id, UpdateRestaurantDTO model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = await _service.GetByIdAsync(id);
                if (restaurant == null) return NotFound(new AuthResponseDTO
                                        {
                                            Status = false,
                                            Message = "Restaurant Not Found"
                                        });

                var result = await _service.UpdateAsync(id, model);
                if (result == false) return BadRequest(new AuthResponseDTO
                                     {
                                         Status = false,
                                         Message = "Restaurant Update Failed"
                                     });

                return Ok(new AuthResponseDTO
                {
                    Status = true,
                    Message = "Restaurant Updated Successfully"
                });
            }

            return BadRequest(false);
        }
        [HttpDelete("Remove/{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return BadRequest(user);

            bool res = await _service.DeleteAsync(id);
            if (res == false) return BadRequest(res);

            return Ok(res);
        }
    }
}