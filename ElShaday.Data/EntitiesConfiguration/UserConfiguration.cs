using ElShaday.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElShaday.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
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
        
        builder.OwnsOne(x => x.Password)
            .Property(y => y.Hash)
            .HasMaxLength(128)
            .IsRequired()
            .HasColumnName("PasswordHash");
        
        builder.OwnsOne(x => x.Password)
            .Property(y => y.Salt)
            .HasMaxLength(255)
            .IsRequired()
            .HasColumnName("PasswordSalt");

        builder.Property(x => x.Role)
            .HasColumnName("Role")
            .HasColumnType("tinyint")
            .IsRequired();
    }
}