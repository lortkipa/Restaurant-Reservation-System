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
        Task<Role> GetRoleById(int id);
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

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles
                .Include(r => r.RoleUsers)
                .ThenInclude(ru => ru.User)
                 .FirstOrDefaultAsync(r => r.Id == id);
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
