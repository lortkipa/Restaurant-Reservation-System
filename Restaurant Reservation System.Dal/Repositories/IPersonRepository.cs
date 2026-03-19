using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
    }

    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly RestaurantContext _context;
        public PersonRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
