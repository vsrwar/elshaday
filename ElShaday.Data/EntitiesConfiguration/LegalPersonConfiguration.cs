using ElShaday.Domain.Entities.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class LegalPersonConfiguration : IEntityTypeConfiguration<LegalPerson>
{
    public void Configure(EntityTypeBuilder<LegalPerson> builder)
    {
        builder.ToTable("LegalPeople");
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
            .HasForeignKey<LegalPerson>(x => x.AddressId)
            .IsRequired();

        builder.Property(x => x.Qualifier)
            .HasColumnName("Qualifier")
            .HasColumnType("tinyint")
            .IsRequired();
    }
}