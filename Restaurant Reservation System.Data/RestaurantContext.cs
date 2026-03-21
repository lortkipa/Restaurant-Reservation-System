using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data.Configurations;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
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
        public DbSet<ReservationStatus> Status { get; set; }
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
            modelBuilder.ApplyConfiguration(new ReservationStatusConfiguration());
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
                    Id = 2, Name = "Worker"
                },
                new Role 
                {
                    Id = 3, Name = "Customer"
                }
            );
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Nikoloz",
                    LastName = "Lortkipanidze",
                    Phone = "577711705",
                    Address = "Near Lisi Lake"
                }
            );
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("Nikoloz123"));
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        PersonId = 1,
                        Username = "NikolozLortki",
                        PasswordHash = hashedPassword,
                        Email = "nikusha191208@gmail.com",
                        RegistrationDate = new DateTime(2026, 3, 20)
                    }
                );
            }
            modelBuilder.Entity<RoleUser>().HasData(
                new RoleUser
                {
                    Id = 1,
                    RoleId = 1,
                    UserId = 1
                }
            );
            modelBuilder.Entity<ReservationStatus>().HasData(
                new ReservationStatus
                {
                    Id = 1,
                    Name = "Pending"
                },
                new ReservationStatus
                {
                    Id = 2,
                    Name = "Confirmed"
                },
                new ReservationStatus
                {
                    Id = 3,
                    Name = "Canceled"
                }
            );
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant
                {
                    Id = 1,
                    Name = "Macdonalds",
                    Location = "Tbilisi, Georgia",
                    Email = "mac@gmail.com",
                    TotalTables = 20,
                    SeatsPerTable = 4
                }
            );
        }
    }
}
