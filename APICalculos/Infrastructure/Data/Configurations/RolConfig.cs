using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class RolConfig : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.Property(prop => prop.NombreRol)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
