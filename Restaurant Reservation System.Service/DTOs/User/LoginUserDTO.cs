using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.User
{
    public class LoginUserDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
