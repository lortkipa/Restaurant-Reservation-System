using Restaurant_Reservation_System.Service.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.User
{
    public class UserPersonDTO
    {
        public UserDTO user { get; set; }
        public PersonDTO person { get; set; }
    }
}
