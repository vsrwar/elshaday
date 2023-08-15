using ElShaday.Domain.Entities.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class CustomerPhysicalPersonConfiguration : IEntityTypeConfiguration<CustomerPhysicalPerson>
{
    public void Configure(EntityTypeBuilder<CustomerPhysicalPerson> builder)
    {
        builder.ToTable("CustomerPhysicalPeople");
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
            .HasForeignKey<CustomerPhysicalPerson>(x => x.AddressId)
            .IsRequired();
    }
}