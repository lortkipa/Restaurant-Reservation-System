using Restaurant_Reservation_System.Service.DTOs.Reservation;
using Restaurant_Reservation_System.Service.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDTO> MakeReservation(CreateReservationDTO reservation);
        Task CancelReservation(int reservationId);
        Task<IEnumerable<ReservationDTO>> GetReservationsByCustomer(int customerId);
        Task<IEnumerable<ReservationDTO>> GetReservationsByDate(DateTime date);
        Task<IEnumerable<ReservationDTO>> GetAllReservationAsync();
        Task<ReservationDTO> GetReservationByIdAsync(int Id);
        Task UpdateReservationStatus(int reservationId, ReservationStatuses status);
        Task<ReservationDTO> AddReservationAsync(CreateReservationDTO model);
        Task<bool> UpdateReservationAsync(int Id, UpdateReservationDTO model);
        Task<bool> DeleteReservationAsync(int Id);
        Task<bool> AreSeatsAvailable(int restaurantId, DateTime date, int requestedSeats);
    }
}
