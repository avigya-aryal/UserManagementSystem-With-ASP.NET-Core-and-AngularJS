using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.EntityMapper
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserID).HasName("UserID");
            builder.Property(x => x.UserID).ValueGeneratedOnAdd()
                .HasColumnName("UserID");

           // builder.Property(x => x.Employee)
           //(x => x.EmployeeID)
           //    .HasColumnName("EmployeeID");

        }
    }
}
