using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.EntityMapper
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.DepartmentID)
                .HasName("pk_DepartmentID");
            builder.Property(x => x.DepartmentID).ValueGeneratedOnAdd()
                .HasColumnName("DepartmentID");


        }
    }
}
