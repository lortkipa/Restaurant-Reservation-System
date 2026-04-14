using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Address { get; set; } = null!;

        // Person => User
        public User? User { get; set; }

        // Person => DeveloperInfo
        public DeveloperInfo? DeveloperInfo { get; set; }
    }
}
