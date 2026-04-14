using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.Menu
{
    public class CreateMenuItemDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsAvaiable { get; set; }
    }
}
