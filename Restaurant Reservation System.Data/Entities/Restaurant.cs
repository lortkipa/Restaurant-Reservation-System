using Restaurant_Reservation_System.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("Restaurants")]
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = null!;
        [Required]
        [MaxLength(254)] // 254 is officially max length for email address
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required]
        public int TotalTables { get; set; }
        [Required]
        public int SeatsPerTable { get; set; }

        // Restaurant => Reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        // Restaurant => Menus
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    }
}