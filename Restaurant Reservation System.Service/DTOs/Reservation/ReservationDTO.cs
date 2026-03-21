using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.Reservation
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        public int TableNumber { get; set; }
        public int GuestCount { get; set; }
    }
}
