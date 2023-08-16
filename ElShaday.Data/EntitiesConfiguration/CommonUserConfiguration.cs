using ElShaday.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class CommonUserConfiguration : IEntityTypeConfiguration<CommonUser>
{
    public void Configure(EntityTypeBuilder<CommonUser> builder)
    {
        builder.ToTable("CommonUsers");
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

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.NickName)
            .HasColumnName("NickName")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Active)
            .HasColumnName("Active")
            .IsRequired();
    }
}