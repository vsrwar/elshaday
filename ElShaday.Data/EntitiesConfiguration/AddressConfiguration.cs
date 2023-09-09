using ElShaday.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
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
        
        builder.Property(x => x.Cep)
            .HasMaxLength(8)
            .HasColumnName("Cep");

        builder.Property(x => x.Logradouro)
            .HasMaxLength(255)
            .HasColumnName("Logradouro");

        builder.Property(x => x.Complemento)
            .HasMaxLength(255)
            .HasColumnName("Complemento");

        builder.Property(x => x.Bairro)
            .HasColumnName("Bairro");

        builder.Property(x => x.Localidade)
            .HasMaxLength(255)
            .HasColumnName("Localidade");

        builder.Property(x => x.Uf)
            .HasMaxLength(2)
            .HasColumnName("Uf");

        builder.Property(x => x.Ibge)
            .HasMaxLength(25)
            .HasColumnName("Ibge");

        builder.Property(x => x.Gia)
            .HasMaxLength(25)
            .HasColumnName("Gia");

        builder.Property(x => x.Ddd)
            .HasMaxLength(2)
            .HasColumnName("Ddd");

        builder.Property(x => x.Siafi)
            .HasMaxLength(25)
            .HasColumnName("Siafi");

        builder.Property(x => x.Numero)
            .HasMaxLength(10)
            .IsRequired();
    }
}