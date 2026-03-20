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

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(int adminId)
        {
            RoleDTO role = await _userService.GetRoleById(adminId);
            if (role == null || role.Name != "Admin")
                return Forbid("Logged in user is not Admin");

            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("GetUserById/{adminId:int}/{userId:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int adminId, int userId)
        {
            RoleDTO role = await _userService.GetRoleById(adminId);
            if (role == null || role.Name != "Admin")
                return Forbid("Logged in user is not Admin");

            UserDTO rating = await _userService.GetByIdAsync(userId);
            if (rating == null) return BadRequest("invalid id");
            return Ok(rating);
        }
        [HttpPut("UpdateUser/{adminId:int}/{userId:int}")]
        public async Task<ActionResult<bool>> UpdateUser(int adminId, int userId, UpdateUserDTO model)
        {
            RoleDTO role = await _userService.GetRoleById(adminId);
            if (role == null || role.Name != "Admin")
                return Forbid("Logged in user is not Admin");

            if (ModelState.IsValid == false) return BadRequest(ModelState);

            UserDTO user = await _userService.GetByIdAsync(userId);
            if (user == null) return BadRequest("invalid id");

            bool res = await _userService.UpdateAsync(userId, model);
            if (res == false) return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("DeleteUser/{adminId:int}/{userId:int}")]
        public async Task<ActionResult<bool>> DeleteUser(int adminId, int userId)
        {
            RoleDTO role = await _userService.GetRoleById(adminId);
            if (role == null || role.Name != "Admin")
                return Forbid("Logged in user is not Admin");

            UserDTO user = await _userService.GetByIdAsync(userId);
            if (user == null) return BadRequest("invalid id");

            var result = await _userService.DeleteAsync(userId);
            if (result == false) return BadRequest();
            return Ok();
        }
    }
}
