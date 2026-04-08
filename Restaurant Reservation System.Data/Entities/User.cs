using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Username { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        [MaxLength(254)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required]
        public DateTime RegistrationDate { get; set; }
        public string? ImageUrl { get; set; }

        // User => Reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        // User => Person
        public Person? Person { get; set; }
        // User => RoleUsers
        public ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
        // EmailJS => User
        public EmailJS EmailJS { get; set; } = null!;
    }
}
