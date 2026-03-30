using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Reservation;
using Restaurant_Reservation_System.Service.Enums;
using Restaurant_Reservation_System.Service.Interfaces;

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

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllReservations()
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var reserv = await _reservationService.GetAllReservationAsync();
            return Ok(reserv);

        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var reserv = await _reservationService.GetReservationByIdAsync(id);
            if (reserv == null) return BadRequest();

            return Ok(reserv);

        }

        [HttpGet("GetReservationsByDate/{date:DateTime}")]
        public async Task<IActionResult> GetReservationsByDate(DateTime date)
        {
            var reserv = await _reservationService.GetReservationsByDate(date);
            if (reserv == null) return BadRequest();

            return Ok(reserv);
        }

        //[HttpGet("GetByCostumerId/{adminId:int}/{customerId:int}")]
        [HttpGet("GetByCostumerId/{customerId:int}")]
        public async Task<IActionResult> GetReservationsByCustomer(int customerId)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            var reserv = await _reservationService.GetReservationsByCustomer(customerId);
            if (reserv == null) return BadRequest();

            return Ok(reserv);

        }

        [HttpPost("Add")]
        public async Task<ActionResult<ReservationDTO>> MakeReservation(CreateReservationDTO reservation)
        {
            if (ModelState.IsValid)
            {
                var model = await _reservationService.MakeReservation(reservation);
                if (model == null) return BadRequest("model is null");
                return Ok(model);
            }

            return BadRequest();
        }
        [HttpPut("UpdateStatus/{id:int}")]
        public async Task<ActionResult<bool>> UpdateReservation(int id, ReservationStatuses statusId)
        {
            //if (!await IsAdmin(adminId))
            //    return Forbid("Logged in user is not Admin");

            if (ModelState.IsValid)
            {
                var reserv = await _reservationService.GetReservationByIdAsync(id);
                if (reserv == null) return BadRequest();

                UpdateReservationDTO updateReservationDTO = new UpdateReservationDTO
                {
                    CustomerId = reserv.CustomerId,
                    RestaurantId = reserv.RestaurantId,
                    StatusId = (int)statusId,
                    Date = reserv.Date,
                    TableNumber = reserv.TableNumber,
                    GuestCount = reserv.GuestCount,
                };
                reserv.StatusId = (int)statusId;

                var result = await _reservationService.UpdateReservationAsync(id, updateReservationDTO);
                if (!result) return BadRequest();

                return Ok();
            }

            return BadRequest();
        }
        [HttpPut("Cancel/{id:int}")]
        public async Task<ActionResult<AuthResponseDTO>> CancelReservation(int id)
        {
            await _reservationService.CancelReservation(id);
            return Ok( new AuthResponseDTO
            {
                Status = true,
                Message = "Reservation cancelled successfully"
            });
        }
        [HttpDelete("DeleteReservation/{id:int}")]
        public async Task<ActionResult<bool>> DeleteReservation(int id)
        {
            if (ModelState.IsValid)
            {
                var reserv = await _reservationService.GetReservationByIdAsync(id);
                if (reserv == null) return BadRequest();

                var result = await _reservationService.DeleteReservationAsync(id);
                if (!result) return BadRequest();

                return Ok();
            }

            return BadRequest();
        }
    }
}
