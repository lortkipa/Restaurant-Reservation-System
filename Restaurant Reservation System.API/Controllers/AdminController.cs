using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
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
        private readonly RestaurantContext _context;
            
        public AdminController(IUserService userService, IRoleUserService roleUserService, RestaurantContext context)
        {
            _userService = userService;
            _roleUserService = roleUserService;
            _context = context;
        }

        private async Task<bool> IsAdmin(int adminId)
        {
            var roles = await _userService.GetRolesById(adminId);
            return roles.Any(r => r.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase));
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [Authorize]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("RemoveRole/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> RemoveRole(int id, int roleId)
        {
            var userRole = await _context.RoleUsers
                .FirstOrDefaultAsync(ur => ur.UserId == id && ur.RoleId == roleId);

            if (userRole == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "User does not have this role"
                });

            _context.RoleUsers.Remove(userRole);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Role removed"
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("GiveRole/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> SetUserRole(int id, int roleId)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "User not found"
                });

            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Role not found"
                });

            // check if already assigned
            bool exists = await _context.RoleUsers
                .AnyAsync(ur => ur.UserId == id && ur.RoleId == roleId);

            if (!exists)
            {
                _context.RoleUsers.Add(new RoleUser
                {
                    UserId = id,
                    RoleId = roleId
                });

                await _context.SaveChangesAsync();
            }

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Role assigned"
            });
        }
    }
}
