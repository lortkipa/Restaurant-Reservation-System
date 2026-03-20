using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.Role
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
