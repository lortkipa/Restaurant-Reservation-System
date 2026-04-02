using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Reservation;
using Restaurant_Reservation_System.Service.Enums;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IMapper _mapper;
        private readonly DbContext _context;

        public ReservationService(IReservationRepository ReservRepo, IMapper Mapper, DbContext context)
        {
            _repo = ReservRepo;
            _mapper = Mapper;
            _context = context;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationAsync()
        {
            var reserv = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationDTO>>(reserv);
        }
        public async Task<ReservationDTO> GetReservationByIdAsync(int Id)
        {
            var reserv = await _repo.GetByIdAsync(Id);
            return _mapper.Map<ReservationDTO>(reserv);
        }
        public async Task<bool> UpdateReservationAsync(int Id, UpdateReservationDTO model)
        {
            var reserv = await _repo.GetByIdAsync(Id);
            if (reserv == null) return false;

            reserv = _mapper.Map(model, reserv);
            await _repo.UpdateAsync(reserv);
            return true;
        }
        public async Task<ReservationDTO> AddReservationAsync(CreateReservationDTO model)
        {
            var reserv = _mapper.Map<Reservation>(model);
            await _repo.AddAsync(reserv);
            return _mapper.Map<ReservationDTO>(reserv);

        }
        public async Task<bool> DeleteReservationAsync(int Id)
        {
            var reserv = await _repo.GetByIdAsync(Id);
            if (reserv == null) return false;
            await _repo.DeleteAsync(Id);
            return true;
        }
        public async Task<ReservationDTO> MakeReservation(CreateReservationDTO reservation)
        {
            var user = await _repo.GetUserWithRoles(reservation.CustomerId);

            //var isCustomer = user.RoleUsers
            //    .Any(ru => ru.Role.Name == "Customer");


            var available = await AreSeatsAvailable(reservation.RestaurantId, reservation.Date, reservation.GuestCount);

            if (!available) return null;

            var model = _mapper.Map<Reservation>(reservation);
            model.CustomerId = reservation.CustomerId;
            model.StatusId = reservation.StatusId;
            await _repo.AddAsync(model);
            return _mapper.Map<ReservationDTO>(model);
        }
        public async Task CancelReservation(int reservationId)
        {
            var reserv = await _repo.GetByIdAsync(reservationId);
            if (reserv == null) return;

            reserv.StatusId = (int)ReservationStatuses.Canceled;
            await _repo.UpdateAsync(reserv);
        }
        public async Task<bool> AreSeatsAvailable(int restaurantId, DateTime date, int requestedSeats)
        {
            var restaurant = _context.Set<Restaurant>().FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant == null) return await Task.FromResult(false);

            int totalSeats = restaurant.TotalTables * restaurant.SeatsPerTable;

            int reservedSeats = _context.Set<Reservation>()
                .Where(r => r.RestaurantId == restaurantId && r.Date.Date == date.Date && r.StatusId != (int)ReservationStatuses.Canceled)
                .Sum(r => r.GuestCount);

            return await Task.FromResult((totalSeats - reservedSeats) >= requestedSeats);
        }
        public async Task<IEnumerable<ReservationDTO>> GetReservationsByCustomer(int customerId)
        {
            var user = await _repo.GetUserWithRoles(customerId);

            //var isCustomer = user.RoleUsers
            //    .Any(ru => ru.Role.Name == "Customer");

            //if (!isCustomer) return null;

            var reserv = await _repo.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<ReservationDTO>>(reserv);
        }
        public async Task<IEnumerable<ReservationDTO>> GetReservationsByDate(DateTime date)
        {
            var reserv = await _repo.GetByDateAsync(date);
            return _mapper.Map<IEnumerable<ReservationDTO>>(reserv);
        }
        public async Task UpdateReservationStatus(int reservationId, ReservationStatuses status)
        {
            var reservation = await _repo.GetByIdAsync(reservationId);
            if (reservation == null)
            {
                throw new ArgumentException("Reservation not found.", nameof(reservationId));
            }

            reservation.StatusId = (int)status;
            await _repo.UpdateAsync(reservation);
        }
    }
}
