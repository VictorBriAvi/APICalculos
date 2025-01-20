using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class DetalleVentaConfig : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.HasKey(dv => dv.DetalleVentaId);
            builder.HasOne(dv => dv.Venta)
                .WithMany(v => v.Detalle)
                .HasForeignKey(dv => dv.VentaId);
            builder.HasOne(dv => dv.TipoDeServicio)
                .WithMany(ts => ts.DetalleVentas)
                .HasForeignKey(dv => dv.TipoDeServicioId);
            builder.HasOne(dv => dv.Empleado) 
                .WithMany(e => e.DetalleVentas)
                .HasForeignKey(dv => dv.EmpleadoId);
           
        }
    }
}
