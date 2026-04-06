using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Service.DTOs.EmailJS;
using Restaurant_Reservation_System.Service.Interfaces;
using System.Security.Claims;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailJSController : ControllerBase
    {
        private readonly IEmailJSService _service;

        public EmailJSController(IEmailJSService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<ActionResult<EmailJSDTO>> Get()
        {
            // Get current user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized("Invalid token");

            int userId = int.Parse(userIdClaim.Value);

            return Ok(await _service.GetByUserIdAsync(userId));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<ActionResult<bool>> Update(UpdateEmailJSDTO model)
        {
            // Get current user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized("Invalid token");

            int userId = int.Parse(userIdClaim.Value);

            if (!ModelState.IsValid)
                return BadRequest(false);

            return Ok(await _service.UpdateAsync(userId, model));
        }
    }
}
