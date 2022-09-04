using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.EntityMapper
{
    public class ContactsMap : IEntityTypeConfiguration<Contacts>
    {
        public void Configure(EntityTypeBuilder<Contacts> builder)
        {
            builder.HasKey(x => x.ContactID)
                .HasName("pk_ContactID");
            builder.Property(x => x.ContactID).ValueGeneratedOnAdd()
                .HasColumnName("ContactID");
           
        }
    }
}
