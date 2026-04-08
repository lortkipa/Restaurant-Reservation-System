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
