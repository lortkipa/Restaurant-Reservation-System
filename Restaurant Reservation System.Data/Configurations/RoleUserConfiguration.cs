using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.ToTable("RoleUsers")
                .HasKey(ru => ru.Id);
            builder.Property(ru => ru.UserId)
                .IsRequired();
            builder.Property(ru => ru.RoleId)
                .IsRequired();

            // RoleUsers => User
            builder.HasOne(ru => ru.User)
                .WithMany(u => u.RoleUsers)
                .HasForeignKey(u => u.UserId);
            // RoleUsers => Role
            builder.HasOne(ru => ru.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(r => r.RoleId);
        }
    }
}
