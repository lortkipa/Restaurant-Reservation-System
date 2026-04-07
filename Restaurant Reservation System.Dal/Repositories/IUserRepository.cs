using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllAdminsAsync();
        Task<IEnumerable<User>> GetAllWorkersAsync();
        Task<IEnumerable<User>> GetAllCostumersAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsernameAsync(string username);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly RestaurantContext _context;
        public UserRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAdminsAsync()
        {
            return await _context.Users
               .Where(u => _context.RoleUsers
               .Any(ru => ru.UserId == u.Id &&
               _context.Roles
               .Any(r => r.Id == ru.RoleId && r.Name == "Admin")))
               .ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllWorkersAsync()
        {
            return await _context.Users
               .Where(u => _context.RoleUsers
               .Any(ru => ru.UserId == u.Id &&
               _context.Roles
               .Any(r => r.Id == ru.RoleId && r.Name == "Worker")))
               .ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllCostumersAsync()
        {
            return await _context.Users
                .Where(u => _context.RoleUsers
                .Any(ru => ru.UserId == u.Id &&
                _context.Roles
                .Any(r => r.Id == ru.RoleId && r.Name == "Customer")))
                .ToListAsync();
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
