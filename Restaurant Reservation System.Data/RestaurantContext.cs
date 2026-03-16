using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Restaurant_Reservation_System.Data
{
    public class RestaurantContext : DbContext
    {
        // DBSet Properties

        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> context) : base(context)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // add configurations

            // seeding
        }
    }
}
