using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    public class ReservationStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        // Status => Reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
