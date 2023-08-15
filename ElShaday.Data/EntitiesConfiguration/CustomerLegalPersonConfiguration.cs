﻿using ElShaday.Domain.Entities.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class CustomerLegalPersonConfiguration : IEntityTypeConfiguration<CustomerLegalPerson>
{
    public void Configure(EntityTypeBuilder<CustomerLegalPerson> builder)
    {
        builder.ToTable("CustomerLegalPeople");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime");

        builder.Property(x => x.DeletedAt)
            .HasColumnName("DeletedAt")
            .HasColumnType("datetime");
        
        builder.OwnsOne(x => x.Document)
            .Property(y => y.Value)
            .HasColumnName("Document")
            .IsRequired();

        builder.Property(x => x.CorporateName)
            .HasColumnName("CorporateName")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.FantasyName)
            .HasColumnName("FantasyName")
            .HasMaxLength(255);
        
        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<CustomerLegalPerson>(x => x.AddressId)
            .IsRequired();
    }
}