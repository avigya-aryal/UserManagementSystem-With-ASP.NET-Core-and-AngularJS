using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.EntityMapper
{
    public class ProjectMap:IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ProjectId).HasName("pk_ProjectID");
            entityBuilder.Property(x => x.ProjectId).ValueGeneratedOnAdd()
                .HasColumnName("ProjectID");
        }
    }
}
