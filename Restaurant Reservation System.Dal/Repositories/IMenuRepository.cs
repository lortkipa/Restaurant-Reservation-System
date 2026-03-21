using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
    }
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        private readonly RestaurantContext _context;
        public MenuRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
