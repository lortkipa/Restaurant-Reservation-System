using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations")
                .HasKey(r => r.Id);
            builder.Property(r => r.CustomerId)
                .IsRequired();
            builder.Property(r => r.RestaurantId)
                .IsRequired();
            builder.Property(r => r.StatusId)
                .IsRequired();
            builder.Property(r => r.Date)
                .IsRequired()
                .HasColumnType("datetime");
            builder.Property(r => r.TableNumber)
                .IsRequired();
            builder.Property(r => r.GuestCount)
                .IsRequired();

            // Reservations => Restaurant
            builder.HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            // Reservations => User
            builder.HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            // Reservations => Status
            builder.HasOne(r => r.Status)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
