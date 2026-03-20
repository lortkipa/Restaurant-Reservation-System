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
    }
    public class RoleUserRepository : BaseRepository<RoleUser>, IRoleUserRepository
    {
        private readonly RestaurantContext _context;
        public RoleUserRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
