using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.Menu
{
    public class UpdateMenuDTO
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;
    }
}
