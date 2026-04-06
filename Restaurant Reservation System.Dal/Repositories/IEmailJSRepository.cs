using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IEmailJSRepository : IBaseRepository<EmailJS>
    {
        Task<EmailJS> GetByUserIdAsync(int id);
    }
    public class EmailJSRepository : BaseRepository<EmailJS>, IEmailJSRepository
    {
        private readonly RestaurantContext _context;
        public EmailJSRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EmailJS> GetByUserIdAsync(int id)
        {
            var emailJS = await _context.EmailJS.FindAsync(id);
            if (emailJS == null)
            { 
                emailJS = new EmailJS
                            {
                                Id = 0,
                                UserId = id,
                                ServiceId = null,
                                TemplateId = null,
                                PublicKey = null
                            };
                await _context.EmailJS.AddAsync(emailJS);
            }
            return emailJS;
        }
    }
}
