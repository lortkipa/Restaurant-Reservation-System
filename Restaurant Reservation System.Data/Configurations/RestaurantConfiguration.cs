using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurants")
                .HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(true);
            builder.HasIndex(r => r.Name)
                .IsUnique();
            builder.Property(r => r.Description)
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(r => r.Location)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(254);
            builder.Property(r => r.TotalTables)
                .IsRequired();
            builder.Property(r => r.SeatsPerTable)
                .IsRequired();

            // Restaurant => Reservations
            builder.HasMany(rest => rest.Reservations)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            // Restaurant => Menus
            builder.HasMany(rest => rest.Menus)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
