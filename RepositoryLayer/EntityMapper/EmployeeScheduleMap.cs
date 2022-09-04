using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.EntityMapper
{
   public class EmployeeScheduleMap:IEntityTypeConfiguration<EmployeeSchedule>
    {
        public void Configure(EntityTypeBuilder<EmployeeSchedule> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ScheduleId).HasName("pk_ScheduleID");
            entityBuilder.Property(x => x.ScheduleId).ValueGeneratedOnAdd()
                .HasColumnName("ScheduleID");

        }
    }
}
