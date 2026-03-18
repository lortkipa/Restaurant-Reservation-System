using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems")
                .HasKey(mi => mi.Id);
            builder.Property(m => m.MenuId)
                .IsRequired();
            builder.Property(mi => mi.Name)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(mi => mi.Price)
                .IsRequired();

            // MenuItems => Menu
            builder.HasOne(mi => mi.Menu)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(mi => mi.MenuId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
