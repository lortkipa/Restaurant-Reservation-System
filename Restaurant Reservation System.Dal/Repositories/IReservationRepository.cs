using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date);
        Task<User> GetUserWithRoles(int userId);

    }

    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Set<Reservation>()
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
        {
            return await _context.Set<Reservation>()
                .Where(r => r.Date.Date == date.Date)
                .ToListAsync();
        }
        public async Task<User> GetUserWithRoles(int userId)
        {
            return await _context.Users
                .Include(u => u.RoleUsers)
                .ThenInclude(ru => ru.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
