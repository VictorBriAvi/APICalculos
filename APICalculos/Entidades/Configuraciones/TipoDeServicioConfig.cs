using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class TipoDeServicioConfig : IEntityTypeConfiguration<TipoDeServicio>
    {
        public void Configure(EntityTypeBuilder<TipoDeServicio> builder)
        {

            builder.HasOne(ur => ur.CategoriasServicios)
                .WithMany(ur => ur.TipoDeServicios)
                .HasForeignKey(ur => ur.CategoriasServiciosId);


            builder.Property(prop => prop.NombreServicio)
                .HasMaxLength(100);


            builder.HasOne(ts => ts.CategoriasServicios)
                .WithMany()
                .HasForeignKey(ts => ts.CategoriasServiciosId);

        }
    }
}
