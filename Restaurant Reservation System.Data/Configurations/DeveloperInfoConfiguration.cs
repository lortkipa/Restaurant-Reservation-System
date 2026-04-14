using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class DeveloperInfoConfiguration : IEntityTypeConfiguration<DeveloperInfo>
    {
        public void Configure(EntityTypeBuilder<DeveloperInfo> builder)
        {
            builder.ToTable("DeveloperInfos")
                .HasKey(d => d.Id);
            builder.Property(d => d.PersonId)
                .IsRequired();
            builder.Property(d => d.Role)
                .IsRequired()
                .HasMaxLength(100);
            //builder.Property(d => d.Bio)
            //    .IsRequired()
            //    .HasMaxLength(500);

            // DeveloperInfo => Person
            builder.HasOne(d => d.Person)
                .WithOne(p => p.DeveloperInfo)
                .HasForeignKey<DeveloperInfo>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
