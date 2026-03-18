using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(u => u.Id);
            builder.Property(r => r.PersonId)
                .IsRequired();
            builder.Property(r => r.Username)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(u => u.PasswordHash)
               .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(254);
            builder.Property(u => u.RegistrationDate)
                .IsRequired()
                .HasColumnType("date");

            // User => Reservations
            builder.HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // User => Person
            builder.HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
            // User => RoleUsers
            builder.HasMany(u => u.RoleUsers)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
