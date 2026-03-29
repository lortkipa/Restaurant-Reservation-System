using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.User;
using Restaurant_Reservation_System.Service.Interfaces;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetProfile/{id:int}")]
        public async Task<UserPersonDTO> GetProfile(int id)
        {
            return await _service.GetByIdAsync(id);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                AuthResponseDTO res = await _service.RegisterAsync(model);
                if (res.Status == false) return BadRequest(res);
                return Ok(res);
            }
            return BadRequest(
                new AuthResponseDTO      
                {
                    Status = false,
                    Message = "Registration Failed"
                }
            );
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginUserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new AuthResponseDTO              
                    {
                        Status = false,
                        Message = "Invalid Form"
                    }
                );

            AuthResponseDTO res = await _service.LoginAsync(model);

            if (!res.Status)
                return BadRequest(res);

            // Get user by username and await the result
            var user = await _service.GetByUsernameAsync(model.Username);

            return Ok(
                    new AuthResponseDTO
                    {
                        Status = true,
                        Message = user.Id.ToString()
                    }
                );
        }
        [HttpPost("Logout")]
        public async Task<ActionResult<AuthResponseDTO>> Logout(int id)
        {
            UserPersonDTO user = await _service.GetByIdAsync(id);
                if (user == null) return NotFound("User doesn't exist");

            return new AuthResponseDTO
            {
                Status = true,
                Message = "Logged out successfully"
            };
        }
        [HttpPut("UpdateProfile/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> Update(int id, UpdateUserDTO model)
        {
            UserPersonDTO user = await _service.GetByIdAsync(id);
            if (user == null) return
                    BadRequest(new AuthResponseDTO
                    {
                        Status = false,
                        Message = "user doesn't exist"
                    });

            bool res = await _service.UpdateAsync(id, model);
            if (res == false) return BadRequest(new AuthResponseDTO
            {
                Status = false,
                Message = "update failed"
            });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "updated successfully"
            });
        }
        [HttpDelete("DeleteProfile/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> Delete(int id)
        {
            UserPersonDTO user = await _service.GetByIdAsync(id);
            if (user == null) return
                    BadRequest( new AuthResponseDTO
                    {
                        Status = false,
                        Message = "user doesn't exist"
                    });

            bool res = await _service.DeleteAsync(id);
            if (res == false) return BadRequest(new AuthResponseDTO
                   {
                       Status = false,
                       Message = "delete failed"
                   });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "deleted successfully"
            });
        }
    }
}
