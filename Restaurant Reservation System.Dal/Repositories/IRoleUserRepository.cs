using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IRoleUserRepository : IBaseRepository<RoleUser>
    {
        Task<IEnumerable<Role>> GetRolesByUserId(int userId);
        Task<bool> SetRoleByUserId(int userId, int roleId);
        Task<bool> RemoveRoleByUserId(int userId, int roleId);
        Task DeleteByUserAndRoleId(int userId, int roleId);
    }
    public class RoleUserRepository : BaseRepository<RoleUser>, IRoleUserRepository
    {
        private readonly RestaurantContext _context;
        public RoleUserRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRolesByUserId(int userId)
        {
            return await _context.RoleUsers
                .Where(ru => ru.UserId == userId)
                .Select(ru => ru.Role)
                .ToListAsync();
        }
        public async Task<bool> SetRoleByUserId(int userId, int roleId)
        {
            var exists = await _context.RoleUsers
                .AnyAsync(ru => ru.UserId == userId && ru.RoleId == roleId);

            if (exists) return false;

            var roleUser = new RoleUser
            {
                UserId = userId,
                RoleId = roleId
            };

            await _context.RoleUsers.AddAsync(roleUser);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> RemoveRoleByUserId(int userId, int roleId)
        {
            var roleUser = await _context.RoleUsers
                .FirstOrDefaultAsync(ru => ru.UserId == userId && ru.RoleId == roleId);

            if (roleUser == null) return false; 

            _context.RoleUsers.Remove(roleUser);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task DeleteByUserAndRoleId(int userId, int roleId)
        {
            var roleUser = await _context.RoleUsers
                .FirstOrDefaultAsync(ru => ru.UserId == userId && ru.RoleId == roleId);

            if (roleUser == null) return; 

            _context.RoleUsers.Remove(roleUser);
            await _context.SaveChangesAsync();
        }
    }
}
