using ElShaday.Domain.Entities.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class PhysicalPersonConfiguration : IEntityTypeConfiguration<PhysicalPerson>
{
    public void Configure(EntityTypeBuilder<PhysicalPerson> builder)
    {
        builder.ToTable("PhysicalPeople");
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

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.NickName)
            .HasColumnName("NickName")
            .HasMaxLength(50);

        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<PhysicalPerson>(x => x.AddressId)
            .IsRequired();

        builder.Property(x => x.Qualifier)
            .HasColumnName("Qualifier")
            .HasColumnType("tinyint")
            .IsRequired();
    }
}