using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.EntityMapper
{
    public class LeaveLogMap : IEntityTypeConfiguration<LeaveLog>
    {
        public void Configure(EntityTypeBuilder<LeaveLog> builder)
        {
            builder.HasKey(x => x.LeaveLogID);
            builder.Property(x => x.LeaveLogID).ValueGeneratedOnAdd()
                 .HasColumnName("LeaveLogID");
        }
    }

}