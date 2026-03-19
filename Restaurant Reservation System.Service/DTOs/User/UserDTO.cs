using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        // public int PersonId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        // public string PasswordHash { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
    }
}
