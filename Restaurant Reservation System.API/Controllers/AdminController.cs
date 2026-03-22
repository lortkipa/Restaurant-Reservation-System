using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.User;
using Restaurant_Reservation_System.Service.Enums;
using Restaurant_Reservation_System.Service.Interfaces;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleUserService _roleUserService;

        public AdminController(IUserService userService, IRoleUserService roleUserService)
        {
            _userService = userService;
            _roleUserService = roleUserService;
        }

        private async Task<bool> IsAdmin(int adminId)
        {
            var roles = await _userService.GetRolesById(adminId);
            return roles.Any(r => r.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var users = await _userService.GetAllCostumersAsync();
            return Ok(users);
        }
        [HttpGet("GetUserById/{id:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
        [HttpGet("GetUserRolesById/{id:int}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserRolesById(int id)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var roles = await _userService.GetRolesById(id);
            if (roles == null)
                return NotFound("User has no roles");

            return Ok(roles);
        }
        [HttpPut("UpdateUserProfile/{id:int}")]
        public async Task<ActionResult<bool>> UpdateUserProfile(int id, UpdateUserDTO model)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            var result = await _userService.UpdateAsync(id, model);
            if (!result)
                return BadRequest("Update failed");

            return Ok(result);
        }
        [HttpDelete("DeleteUserProfile/{id:int}")]
        public async Task<ActionResult> DeleteUserProfile(int id)
        {
            //if (!await IsAdmin(id))
            //    return Forbid("Logged in user is not Admin");

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            var result = await _userService.DeleteAsync(id);
            if (!result)
                return BadRequest("Delete failed");

            return Ok();
        }
    }
}
