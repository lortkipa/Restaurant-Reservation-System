using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Reservation;
using Restaurant_Reservation_System.Service.Enums;
using Restaurant_Reservation_System.Service.Interfaces;
using System.Security.Claims;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

        public ReservationController(IReservationService ReservServ, IUserService userServ)
        {
            _reservationService = ReservServ;
            _userService = userServ;
        }
        private async Task<bool> IsAdmin(int adminId)
        {
            var roles = await _userService.GetRolesById(adminId);
            return roles.Any(r => r.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase));
        }
   
            private int? GetUserId()
            {
                var claim = User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier ||
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/identity/claims/nameidentifier");

                return claim != null ? int.Parse(claim.Value) : null;
            }

            // 🔹 ADMIN ONLY
            [Authorize(Roles = "Admin")]
            [HttpGet("GetAll")]
            public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
            {
                var reserv = await _reservationService.GetAllReservationAsync();
                return Ok(reserv);
            }

        // 🔹 USER OR ADMIN (but user only sees own)
            [Authorize]
            [HttpGet("GetById/{id:int}")]
            public async Task<IActionResult> GetReservationById(int id)
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var reserv = await _reservationService.GetReservationByIdAsync(id);
                if (reserv == null) return NotFound();

                // If not admin, ensure it's their reservation
                if (!User.IsInRole("Admin") && reserv.CustomerId != userId)
                    return Forbid();

                return Ok(reserv);
            }

            [Authorize(Roles = "Admin")]
            [HttpGet("GetReservationsByDate/{date:DateTime}")]
            public async Task<IActionResult> GetReservationsByDate(DateTime date)
            {
                var reserv = await _reservationService.GetReservationsByDate(date);
                return Ok(reserv);
            }

            // 🔹 CURRENT USER ONLY (no customerId param anymore)
            [Authorize(Roles = "Customer")]
            [HttpGet("Get")]
            public async Task<IActionResult> GetMyReservations()
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var reserv = await _reservationService.GetReservationsByCustomer(userId.Value);
                return Ok(reserv);
            }

            // 🔹 CREATE (Customer)
            [Authorize]
            [HttpPost("Add")]
            public async Task<ActionResult<ReservationDTO>> MakeReservation(CreateReservationDTO reservation)
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                reservation.CustomerId = userId.Value; // force correct user

                var model = await _reservationService.MakeReservation(reservation);
                if (model == null) return BadRequest("model is null");

                return Ok(model);
            }

            // 🔹 ADMIN ONLY
            [Authorize(Roles = "Admin")]
            [HttpPut("UpdateStatus/{id:int}")]
            public async Task<ActionResult> UpdateReservation(int id, ReservationStatuses statusId)
            {
                var reserv = await _reservationService.GetReservationByIdAsync(id);
                if (reserv == null) return NotFound();

                var updateReservationDTO = new UpdateReservationDTO
                {
                    CustomerId = reserv.CustomerId,
                    RestaurantId = reserv.RestaurantId,
                    StatusId = (int)statusId,
                    Date = reserv.Date,
                    TableNumber = reserv.TableNumber,
                    GuestCount = reserv.GuestCount,
                };

                var result = await _reservationService.UpdateReservationAsync(id, updateReservationDTO);
                if (!result) return BadRequest();

                return Ok();
            }

            // 🔹 CUSTOMER (own reservation) OR ADMIN
            [HttpPut("Cancel/{id:int}")]
            public async Task<ActionResult<AuthResponseDTO>> CancelReservation(int id)
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var reserv = await _reservationService.GetReservationByIdAsync(id);
                if (reserv == null) return NotFound();

                if (!User.IsInRole("Admin") && reserv.CustomerId != userId)
                    return Forbid();

                await _reservationService.CancelReservation(id);

                return Ok(new AuthResponseDTO
                {
                    Status = true,
                    Message = "Reservation cancelled successfully"
                });
            }

            // 🔹 ADMIN OR OWNER
            [HttpDelete("DeleteReservation/{id:int}")]
            public async Task<ActionResult> DeleteReservation(int id)
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var reserv = await _reservationService.GetReservationByIdAsync(id);
                if (reserv == null) return NotFound();

                if (!User.IsInRole("Admin") && reserv.CustomerId != userId)
                    return Forbid();

                var result = await _reservationService.DeleteReservationAsync(id);
                if (!result) return BadRequest();

                return Ok();
            }
        
    }
}
