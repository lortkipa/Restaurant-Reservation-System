using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.EmailJS;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.User;
using Restaurant_Reservation_System.Service.Helpers;
using Restaurant_Reservation_System.Service.Interfaces;
using System.Security.Claims;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public readonly IPersonService _personalService;
        private readonly RestaurantContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailJSService _emailJSService;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;

        public UserController(IWebHostEnvironment env, IImageService imgService, IEmailJSService emailJSService, IConfiguration config, IUserService service, IPersonService personService, RestaurantContext context)
        {
            _env = env;
            _imageService = imgService;
            _emailJSService = emailJSService;
            _service = service;
            _personalService = personService;
            _context = context;
            _config = config;
        }

        [Authorize(Roles = "Admin,Worker")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin,Worker")]
        [HttpGet("GetAllAdmins")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllAdmins()
        {
            var users = await _service.GetAllAdminsAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin,Worker")]
        [HttpGet("GetAllWorkers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllWorkers()
        {
            var users = await _service.GetAllWorkersAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin,Worker")]
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var users = await _service.GetAllCostumersAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUserById/{id:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var user = await _service.GetByIdAsync(id);
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

            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            var result = await _service.UpdateWithoutPasswordAsync(id, model);
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

            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            var result = await _service.DeleteAsync(id);
            if (!result)
                return BadRequest("Delete failed");

            return Ok();
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("RemoveRole/{id:int}/{roleId:int}")]
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
        //[Authorize(Roles = "Admin")]
        [HttpPost("SetRole/{id:int}/{roleId:int}")]
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
        //[Authorize(Roles = "Admin")]
        [HttpGet("GetRoles/{id:int}")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetUserRoles(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found");
            var roles = await _service.GetRolesById(id);
            if (roles == null || !roles.Any())
                return NotFound("User has no roles");
            return Ok(roles);
        }
        [Authorize]
        [HttpGet("GetRolesById")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetUserRolesById()
        {
            // Get current user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized("Invalid token");

            int userId = int.Parse(userIdClaim.Value);

            // Fetch roles from service
            var roles = await _service.GetRolesById(userId);
            if (roles == null || !roles.Any())
                return NotFound("User has no roles");

            return Ok(roles);
        }
        [Authorize]
        [HttpGet("GetProfile")]
        public async Task<ActionResult<UserPersonDTO>> GetProfile()
        {
            // Extract user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var profile = await _service.GetByIdAsync(userId);
            return Ok(profile);
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                AuthResponseDTO res = await _service.RegisterAsync(model);
                if (res.Status == false) return BadRequest(res);

                //_emailJSService.CreateAsync(new CreateEmailJSDTO
                //{
                //    UserId = 
                //});

                var user = await _service.GetByUsernameAsync(model.Username);
                if (user == null) return BadRequest(user);

                await _emailJSService.CreateAsync(new CreateEmailJSDTO
                {
                    UserId = user.Id,
                    ServiceId = null,
                    TemplateId = null,
                    PublicKey = null
                });

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
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.LoginAsync(model);
                if (result.Status)
                {
                    var loggedUser = await _service.GetByUsernameAsync(model.Username);
                    List<RoleDTO> userRoles = (await _service.GetRolesById(loggedUser.Id)).ToList();
                    var token = TokenHelper.TokenGeneration(loggedUser.Id, loggedUser.Username, userRoles, _config);

                    HttpContext.Response.Cookies.Append("Token", token);

                    return Ok(new AuthResponseDTO { Status = true, Message = token });
                }

                return Unauthorized(result.Message);
            }

            return BadRequest();
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult<AuthResponseDTO>> Logout(string token)
        {
            HttpContext.Response.Cookies.Delete("Token");

            return new AuthResponseDTO
            {
                Status = true,
                Message = "Logged out successfully"
            };
        }
        [Authorize]
        [HttpPut("UpdateProfile")]
        public async Task<ActionResult<AuthResponseDTO>> UpdateProfile(UpdateUserDTO model)
        {
            // Get current user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Invalid token"
                });

            int userId = int.Parse(userIdClaim.Value);

            // Get user
            UserPersonDTO user = await _service.GetByIdAsync(userId);
            if (user == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "User doesn't exist"
                });

            // Update profile
            bool res = await _service.UpdateAsync(userId, model);
            if (!res)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Update failed"
                });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Profile updated"
            });
        }
        [Authorize]
        [HttpPut("UpdatePersonalInfo")]
        public async Task<ActionResult<AuthResponseDTO>> UpdatePersonalInfo(UpdatePersonDTO model)
        {
            // Get current user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Invalid token"
                });

            int userId = int.Parse(userIdClaim.Value);

            // Get the user from the service
            UserPersonDTO user = await _service.GetByIdAsync(userId);
            if (user == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "User doesn't exist"
                });

            // Update personal info
            bool res = await _personalService.UpdateAsync(user.person.Id, model);
            if (!res)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Update failed"
                });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Personal info updated"
            });
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("DeleteProfile")]
        public async Task<ActionResult<AuthResponseDTO>> Delete()
        {
            // Get current user ID from JWT claims
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

            if (userIdClaim == null)
                return Unauthorized(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Invalid token"
                });

            int userId = int.Parse(userIdClaim.Value);

            // Get the user from the service
            UserPersonDTO user = await _service.GetByIdAsync(userId);
            if (user == null)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "User doesn't exist"
                });

            // Delete user
            bool res = await _service.DeleteAsync(userId);
            if (!res)
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = "Delete failed"
                });

            return Ok(new AuthResponseDTO
            {
                Status = true,
                Message = "Deleted successfully"
            });
        }

        [Authorize]
        [HttpPost("UpdateProfilePicture")]
        public async Task<ActionResult<AuthResponseDTO>> UpdateProfilePicture(IFormFile? file)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c =>
                        c.Type == ClaimTypes.NameIdentifier ||
                        c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");
                if (userIdClaim == null)
                    return Unauthorized(new AuthResponseDTO
                    {
                        Status = false,
                        Message = "User Not Logged In"
                    });
                int userId = int.Parse(userIdClaim.Value);
                if (file == null)
                {
                    var res = await _service.UpdatePictureAsync(userId, null);
                    if (!res)
                        return BadRequest(new AuthResponseDTO
                        {
                            Status = false,
                            Message = "Profile Picture Not Updated"
                        });
                } else 
                {
                    string? imagePath = await _imageService.SaveImageAsync(file, "users", _env.WebRootPath);
                    var res = await _service.UpdatePictureAsync(userId, imagePath);
                    if (!res)
                        return BadRequest(new AuthResponseDTO
                        {
                            Status = false,
                            Message = "Profile Picture Not Updated"
                        }); ;
                }

                return Ok(new AuthResponseDTO
                {
                    Status = true,
                    Message = "Profile Picture Updated"
                });
            } 
            catch(Exception ex)
            {
                return BadRequest(new AuthResponseDTO
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
    }
}
