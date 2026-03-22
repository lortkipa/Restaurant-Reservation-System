using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
    }
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        private readonly RestaurantContext _context;
        public RestaurantRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
