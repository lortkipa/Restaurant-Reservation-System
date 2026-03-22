using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.Restaurant
{
    public class UpdateRestaurantDTO
    {
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TotalTables { get; set; }
        public int SeatsPerTable { get; set; }
    }
}
