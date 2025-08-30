using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class DetalleVentaConfig : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.HasKey(dv => dv.DetalleVentaId);

            // Relación con Venta: Cascade, borra los detalles si se borra la venta
            builder.HasOne(dv => dv.Venta)
                .WithMany(v => v.Detalle)
                .HasForeignKey(dv => dv.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con TipoDeServicio: Restrict, no se puede borrar un servicio usado
            builder.HasOne(dv => dv.TipoDeServicio)
                .WithMany(ts => ts.DetalleVentas)
                .HasForeignKey(dv => dv.TipoDeServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Employee: Restrict, no se borran ventas históricas si se borra un empleado
            builder.HasOne(dv => dv.Empleado)
                .WithMany(e => e.DetalleVentas)
                .HasForeignKey(dv => dv.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
