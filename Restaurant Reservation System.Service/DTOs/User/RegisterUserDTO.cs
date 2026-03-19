using Restaurant_Reservation_System.Service.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.User
{
    public class RegisterUserDTO
    {
        public CreatePersonDTO? Person { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
