using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CostumerId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        [Column("Date")]
        public DateTime Date { get; set; }
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int GuestCount { get; set; }

        // Reservations => Restaurant
        public Restaurant? Restaurant { get; set; }
        // Reservations => User
        // public User? User { get; set; }
    }
}
