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
        public DbSet<EmailJS> EmailJS { get; set; }

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
            modelBuilder.ApplyConfiguration(new EmailJSConfiguration());

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
                    Phone = "577711701",
                    Address = "Near Lisi Lake"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Temo",
                    LastName = "Totoshvili",
                    Phone = "577711702",
                    Address = "Address #2"
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Davit",
                    LastName = "Papava",
                    Phone = "577711703",
                    Address = "Address #3"
                },
                new Person
                {
                    Id = 4,
                    FirstName = "Demetre",
                    LastName = "Kvirikashvili",
                    Phone = "577711704",
                    Address = "Address #4"
                },
                new Person
                {
                    Id = 5,
                    FirstName = "Saba",
                    LastName = "Dolidze",
                    Phone = "577711705",
                    Address = "Address #5"
                }
            );
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("admin"));
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        PersonId = 1,
                        Username = "NikolozLortki",
                        PasswordHash = hashedPassword,
                        Email = "nikusha191208@gmail.com",
                        RegistrationDate = new DateTime(2026, 3, 20),
                        ImageUrl = "uploads/users/nikoloz-lortkipanidze.jpg"
                    },
                    new User
                    {
                        Id = 2,
                        PersonId = 2,
                        Username = "Temo_totoshvili",
                        PasswordHash = hashedPassword,
                        Email = "totoshvili@gmail.com",
                        RegistrationDate = new DateTime(2026, 3, 20),
                        ImageUrl = null,
                    },
                    new User
                    {
                        Id = 3,
                        PersonId = 3,
                        Username = "DatoPapava",
                        PasswordHash = hashedPassword,
                        Email = "papava@gmail.com",
                        RegistrationDate = new DateTime(2026, 3, 20),
                        ImageUrl = null,
                    },
                    new User
                    {
                        Id = 4,
                        PersonId = 4,
                        Username = "Kvirrik",
                        PasswordHash = hashedPassword,
                        Email = "kvirrik@gmail.com",
                        RegistrationDate = new DateTime(2026, 3, 20),
                        ImageUrl = null,
                    },
                    new User
                    {
                        Id = 5,
                        PersonId = 5,
                        Username = "SabaDolidze",
                        PasswordHash = hashedPassword,
                        Email = "SabaDolidze@gmail.com",
                        RegistrationDate = new DateTime(2026, 3, 20),
                        ImageUrl = null,
                    }
                );
            }
            modelBuilder.Entity<EmailJS>().HasData(
                new EmailJS
                {
                    Id = 1,
                    UserId = 1,
                    ServiceId = "service_kqw395h",
                    TemplateId = "template_75iei9r",
                    PublicKey = "90LyXpeSeVnNPQeFJ"
                },
                new EmailJS
                {
                    Id = 2,
                    UserId = 2,
                    ServiceId = null,
                    TemplateId = null,
                    PublicKey = null
                },
                new EmailJS
                {
                    Id = 3,
                    UserId = 3,
                    ServiceId = null,
                    TemplateId = null,
                    PublicKey = null
                },
                new EmailJS
                {
                    Id = 4,
                    UserId = 4,
                    ServiceId = null,
                    TemplateId = null,
                    PublicKey = null
                },
                new EmailJS
                {
                    Id = 5,
                    UserId = 5,
                    ServiceId = null,
                    TemplateId = null,
                    PublicKey = null
                }
            );
            modelBuilder.Entity<RoleUser>().HasData(
                new RoleUser
                {
                    Id = 1,
                    RoleId = 1,
                    UserId = 1
                },
                new RoleUser
                {
                    Id = 2,
                    RoleId = 1,
                    UserId = 2
                },
                new RoleUser
                {
                    Id = 3,
                    RoleId = 1,
                    UserId = 3
                },
                new RoleUser
                {
                    Id = 4,
                    RoleId = 1,
                    UserId = 4
                },
                new RoleUser
                {
                    Id = 5,
                    RoleId = 1,
                    UserId = 5
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
                 TotalTables = 4,
                 SeatsPerTable = 2
             },
             new Restaurant
             {
                 Id = 2,
                 Name = "Barbarestan",
                 Location = "Tbilisi, 132 Davit Aghmashenebeli Ave",
                 Description = "Historic recipes from a 19th-century cookbook served in an elegant setting.",
                 Email = "barbarestan@example.com",
                 TotalTables = 12,
                 SeatsPerTable = 3
             },
             new Restaurant
             {
                 Id = 3,
                 Name = "Keto and Kote",
                 Location = "Tbilisi, 3 Mikheil Zandukeli St",
                 Description = "Charming garden restaurant known for traditional dishes and romantic vibes.",
                 Email = "ketoandkote@example.com",
                 TotalTables = 10,
                 SeatsPerTable = 5
             },
             new Restaurant
             {
                 Id = 4,
                 Name = "Salobie Bia",
                 Location = "Mtskheta, 1 Samtavro St",
                 Description = "Famous for authentic lobio and rustic Georgian comfort food.",
                 Email = "salobie@example.com",
                 TotalTables = 8,
                 SeatsPerTable = 2
             },
             new Restaurant
             {
                 Id = 5,
                 Name = "Machakhela",
                 Location = "Batumi, 26 May 6 St",
                 Description = "Casual spot popular for Adjarian khachapuri and local favorites.",
                 Email = "machakhela@example.com",
                 TotalTables = 3,
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
            modelBuilder.Entity<Menu>().HasData(
    // Shavi Lomi
    new Menu { Id = 1, RestaurantId = 1, Name = "Main Dishes" },
    new Menu { Id = 2, RestaurantId = 1, Name = "Drinks" },

    // Barbarestan
    new Menu { Id = 3, RestaurantId = 2, Name = "Traditional Meals" },
    new Menu { Id = 4, RestaurantId = 2, Name = "Wine & Drinks" },

    // Keto and Kote
    new Menu { Id = 5, RestaurantId = 3, Name = "Garden Specials" },
    new Menu { Id = 6, RestaurantId = 3, Name = "Desserts" },

    // Salobie Bia
    new Menu { Id = 7, RestaurantId = 4, Name = "Lobio & Beans" },
    new Menu { Id = 8, RestaurantId = 4, Name = "Extras" },

    // Machakhela
    new Menu { Id = 9, RestaurantId = 5, Name = "Khachapuri" },
    new Menu { Id = 10, RestaurantId = 5, Name = "Drinks" },

    // Heart of Batumi
    new Menu { Id = 11, RestaurantId = 6, Name = "Georgian Classics" },
    new Menu { Id = 12, RestaurantId = 6, Name = "Drinks" }
);
            modelBuilder.Entity<MenuItem>().HasData(

    // Shavi Lomi
    new MenuItem { Id = 1, MenuId = 1, Name = "Chashushuli", Price = 18.5M, IsAvaiable = true },
    new MenuItem { Id = 2, MenuId = 1, Name = "Ojakhuri", Price = 16.0M, IsAvaiable = true },
    new MenuItem { Id = 3, MenuId = 2, Name = "Red Wine", Price = 12.0M, IsAvaiable = true },
    new MenuItem { Id = 4, MenuId = 2, Name = "Craft Beer", Price = 8.5M, IsAvaiable = true },

    // Barbarestan
    new MenuItem { Id = 5, MenuId = 3, Name = "Kharcho", Price = 20.0M, IsAvaiable = true },
    new MenuItem { Id = 6, MenuId = 3, Name = "Chkmeruli", Price = 19.5M, IsAvaiable = true },
    new MenuItem { Id = 7, MenuId = 4, Name = "White Wine", Price = 13.0M, IsAvaiable = true },
    new MenuItem { Id = 8, MenuId = 4, Name = "Mineral Water", Price = 3.0M, IsAvaiable = true },

    // Keto and Kote
    new MenuItem { Id = 9, MenuId = 5, Name = "Mtsvadi", Price = 17.0M, IsAvaiable = true },
    new MenuItem { Id = 10, MenuId = 5, Name = "Badrijani Nigvzit", Price = 11.0M, IsAvaiable = true },
    new MenuItem { Id = 11, MenuId = 6, Name = "Churchkhela", Price = 6.0M, IsAvaiable = true },
    new MenuItem { Id = 12, MenuId = 6, Name = "Honey Cake", Price = 7.5M, IsAvaiable = true },

    // Salobie Bia
    new MenuItem { Id = 13, MenuId = 7, Name = "Lobio (Clay Pot)", Price = 9.0M, IsAvaiable = true },
    new MenuItem { Id = 14, MenuId = 7, Name = "Lobio with Mchadi", Price = 11.0M, IsAvaiable = true },
    new MenuItem { Id = 15, MenuId = 8, Name = "Pickled Vegetables", Price = 5.5M, IsAvaiable = true },
    new MenuItem { Id = 16, MenuId = 8, Name = "Cornbread (Mchadi)", Price = 3.5M, IsAvaiable = true },

    // Machakhela
    new MenuItem { Id = 17, MenuId = 9, Name = "Adjarian Khachapuri", Price = 14.0M, IsAvaiable = true },
    new MenuItem { Id = 18, MenuId = 9, Name = "Imeretian Khachapuri", Price = 12.0M, IsAvaiable = true },
    new MenuItem { Id = 19, MenuId = 10, Name = "Lemonade", Price = 4.0M, IsAvaiable = true },
    new MenuItem { Id = 20, MenuId = 10, Name = "Beer", Price = 6.5M, IsAvaiable = true },

    // Heart of Batumi
    new MenuItem { Id = 21, MenuId = 11, Name = "Khinkali (10 pcs)", Price = 13.0M, IsAvaiable = true },
    new MenuItem { Id = 22, MenuId = 11, Name = "Chakapuli", Price = 18.0M, IsAvaiable = true },
    new MenuItem { Id = 23, MenuId = 12, Name = "Red Wine", Price = 11.0M, IsAvaiable = true },
    new MenuItem { Id = 24, MenuId = 12, Name = "Cola", Price = 3.0M, IsAvaiable = true }
);
        }
    }
}
