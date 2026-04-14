using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons")
                .HasKey(p => p.Id);
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(true);
            builder.Property(p => p.LastName)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode(true);
            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            // Person => User
            builder.HasOne(p => p.User)
                .WithOne(u => u.Person)
                .HasForeignKey<User>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
            // Person => DeveloperInfo
            builder.HasOne(p => p.DeveloperInfo)
                .WithOne(d => d.Person)
                .HasForeignKey<DeveloperInfo>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
