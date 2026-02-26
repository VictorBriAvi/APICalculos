using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FullName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Username)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.CreatedOn)
                .IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();
        }
    }

}
