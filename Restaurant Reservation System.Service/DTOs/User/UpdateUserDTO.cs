using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.User
{
    public class UpdateUserDTO
    {
        // public int PersonId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ImageUrl { get; set; }
        // public DateTime RegistrationDate { get; set; }
    }
}
