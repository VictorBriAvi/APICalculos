using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Entidades.Configuraciones
{
    public class CategoriasServiciosConfig : IEntityTypeConfiguration<CategoriasServicios>
    {
        public void Configure(EntityTypeBuilder<CategoriasServicios> builder)
        {
            builder.HasKey(t => t.CategoriasServiciosId);

            builder.Property(prop => prop.NombreCategoriaServicio)
            .HasMaxLength(100)
            .IsRequired();



        }
    }
}
