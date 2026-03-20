using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.RoleUser
{
    public class CreateRoleUserDTO
    {
        // public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
