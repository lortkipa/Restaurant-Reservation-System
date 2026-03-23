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
                    Name = "Shavi Lomi",
                    Location = "Tbilisi, 30 Zurab Kvlividze St",
                    Description = "Modern Georgian cuisine with creative twists and a cozy, artsy atmosphere.",
                    Email = "shavilomi@example.com",
                    TotalTables = 15,
                    SeatsPerTable = 4
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Barbarestan",
                    Location = "Tbilisi, 132 Davit Aghmashenebeli Ave",
                    Description = "Historic recipes from a 19th-century cookbook served in an elegant setting.",
                    Email = "barbarestan@example.com",
                    TotalTables = 12,
                    SeatsPerTable = 4
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "Keto and Kote",
                    Location = "Tbilisi, 3 Mikheil Zandukeli St",
                    Description = "Charming garden restaurant known for traditional dishes and romantic vibes.",
                    Email = "ketoandkote@example.com",
                    TotalTables = 10,
                    SeatsPerTable = 4
                },
                new Restaurant
                {
                    Id = 4,
                    Name = "Salobie Bia",
                    Location = "Mtskheta, 1 Samtavro St",
                    Description = "Famous for authentic lobio and rustic Georgian comfort food.",
                    Email = "salobie@example.com",
                    TotalTables = 8,
                    SeatsPerTable = 4
                },
                new Restaurant
                {
                    Id = 5,
                    Name = "Machakhela",
                    Location = "Batumi, 26 May 6 St",
                    Description = "Casual spot popular for Adjarian khachapuri and local favorites.",
                    Email = "machakhela@example.com",
                    TotalTables = 14,
                    SeatsPerTable = 4
                },
                new Restaurant
                {
                    Id = 6,
                    Name = "Heart of Batumi",
                    Location = "Batumi, 11 Gen. Mazniashvili St",
                    Description = "Cozy restaurant offering classic Georgian meals in a warm setting.",
                    Email = "heartofbatumi@example.com",
                    TotalTables = 12,
                    SeatsPerTable = 4
                }
            );
        }
    }
}
