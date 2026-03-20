using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                AuthResponseDTO res = await _service.RegisterAsync(model);
                if (res.Status == false) return BadRequest(res);
                return Ok(res);
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginUserDTO model)
        {
            if (ModelState.IsValid)
            {
                AuthResponseDTO res = await _service.LoginAsync(model);
                if (res.Status == false) return BadRequest(res);
                return Ok(res);
            }
            return BadRequest();
        }
    }
}
