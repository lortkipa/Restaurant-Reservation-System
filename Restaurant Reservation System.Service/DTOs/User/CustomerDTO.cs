using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.User
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        // public int PersonId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        // public string PasswordHash { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ReservationDTO> Reservations { get; set; } = new List<ReservationDTO>();
    }
}
