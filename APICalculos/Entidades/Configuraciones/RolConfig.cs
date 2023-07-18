using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Entidades.Configuraciones
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
