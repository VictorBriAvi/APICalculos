using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Entidades.Configuraciones
{
    public class ServicioConfig : IEntityTypeConfiguration<Servicio>
    {
   

        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.Property(prop => prop.ValorServicio)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
