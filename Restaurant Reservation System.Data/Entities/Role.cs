using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        // Role => RoleUsers
        public ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
    }
}
