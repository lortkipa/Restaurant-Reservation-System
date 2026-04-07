using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Service.DTOs.EmailJS;
using Restaurant_Reservation_System.Service.Interfaces;
using System.Security.Claims;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailJSController : ControllerBase
    {
        private readonly RestaurantContext _context;
        private readonly IEmailJSService _service;

        public EmailJSController(IEmailJSService service, RestaurantContext context)
        {
            _context = context;
            _service = service;
        }
        [AllowAnonymous()]
        [HttpGet("GetAll")]
        public async Task<ActionResult<EmailJSDTO[]>> GetAll()
        {
            return Ok(await _service.GetAll());
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

            var emailJs = await _service.GetByUserIdAsync(userId);

            if (emailJs == null)
                return NotFound("EmailJS config not found");

            if (!ModelState.IsValid)
                return BadRequest(false);

            model.UserId = emailJs.UserId;
            var res = await _service.UpdateAsync(emailJs.Id, model);
            return Ok(res);
        }
    }
}
