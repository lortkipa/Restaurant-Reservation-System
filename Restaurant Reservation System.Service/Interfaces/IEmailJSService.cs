using Restaurant_Reservation_System.Service.DTOs.EmailJS;
using Restaurant_Reservation_System.Service.DTOs.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IEmailJSService
    {
        Task<EmailJSDTO> GetByUserIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateEmailJSDTO model);
    }
}
