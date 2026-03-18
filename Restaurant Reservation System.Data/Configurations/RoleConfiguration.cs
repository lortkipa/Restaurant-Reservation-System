using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles")
                .HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(r => r.Name)
                .IsUnique();

            // Role => RoleUsers
            builder.HasMany(r => r.RoleUsers)
                .WithOne(ru => ru.Role)
                .HasForeignKey(ru => ru.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
