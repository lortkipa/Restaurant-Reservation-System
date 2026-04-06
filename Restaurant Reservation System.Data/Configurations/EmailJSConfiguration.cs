using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class EmailJSConfiguration : IEntityTypeConfiguration<EmailJS>
    {
        public void Configure(EntityTypeBuilder<EmailJS> builder)
        {
            builder.ToTable("EmailJSConfigs")
                .HasKey(e => e.Id);
            builder.Property(e => e.UserId)
                .IsRequired();
            builder.HasIndex(e => e.UserId)
                .IsUnique();
            builder.HasIndex(e => e.ServiceId)
                .IsUnique();
            builder.HasIndex(e => e.TemplateId)
                .IsUnique();
            builder.HasIndex(e => e.PublicKey)
                .IsUnique();

            // EmailJS => User
            builder.HasOne(e => e.User)
                .WithOne(u => u.EmailJS)
                .HasForeignKey<EmailJS>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
