using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly RestaurantContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(RestaurantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), typeof(T).Name + "can't be null.");

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), typeof(T).Name + "can't be null.");

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
