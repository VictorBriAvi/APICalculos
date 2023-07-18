using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Entidades.Configuraciones
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(prop => prop.NombreCompletoUsuario)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(prop => prop.NombreDeUsuario)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(prop => prop.Correo)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(prop => prop.Password)
                .HasMaxLength(100)
                .IsRequired();
        }

    }
}
