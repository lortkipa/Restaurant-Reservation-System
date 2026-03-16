using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Infrastructure
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
