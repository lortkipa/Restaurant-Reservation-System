using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.DTOs.EmailJS
{
    public class CreateEmailJSDTO
    {
        public int UserId { get; set; }
        public string? ServiceId { get; set; }
        public string? TemplateId { get; set; }
        public string? PublicKey { get; set; }
    }
}
