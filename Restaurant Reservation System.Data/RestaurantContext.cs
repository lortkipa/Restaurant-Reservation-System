using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data.Configurations;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Restaurant_Reservation_System.Data
{
    public class RestaurantContext : DbContext
    {
        // DBSet Properties
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<User> Users { get; set; }

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
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // seeding
            modelBuilder.Entity<Role>().HasData(
                new Role 
                {
                    Id = 1, Name = "Admin"
                },
                new Role
                {
                    Id = 2, Name = "Manager"
                },
                new Role 
                {
                    Id = 3, Name = "Customer"
                }
            );
        }
    }
}
