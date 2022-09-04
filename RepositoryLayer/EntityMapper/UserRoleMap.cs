using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.EntityMapper
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.RoleID).HasName("RoleID");
            builder.Property(x => x.RoleID).ValueGeneratedOnAdd()
                .HasColumnName("RoleID");

            //builder.Property(x => x.RoleType)
            //    .HasColumnName("RoleType")
            //    .HasColumnType("VARCHAR(50)")
            //    .IsRequired();

            //builder.Property(x => x.CreatedTS)
            //    .HasColumnName("CreatedTS")
            //    .HasColumnType("datetime");

            //builder.Property(x => x.CreatedBy)
            //    .HasColumnName("CreatedBy")
            //    .HasColumnType("INT");

            //builder.Property(x => x.ModifiedTS)
            //    .HasColumnName("ModifiedTS")
            //    .HasColumnType("datetime");

            //builder.Property(x => x.ModifiedBy)
            //    .HasColumnName("ModifiedBy")
            //    .HasColumnType("INT");
        }
    }
}
