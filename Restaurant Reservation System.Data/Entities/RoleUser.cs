using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    public class RoleUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }

        // RoleUsers => User
        public User? User { get; set; }
        // RoleUsers => Role
        public Role? Role { get; set; }
    }
}
