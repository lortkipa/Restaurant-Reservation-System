using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus")
                .HasKey(m => m.Id);
            builder.Property(m => m.RestaurantId)
                .IsRequired();
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(25);

            // Menu => MenuItems
            builder.HasMany(m => m.MenuItems)
                .WithOne(mi => mi.Menu)
                .HasForeignKey(mi => mi.MenuId)
                .OnDelete(DeleteBehavior.Cascade);
            // Menus => Restaurant
            builder.HasOne(m => m.Restaurant)
                .WithMany(r => r.Menus)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
