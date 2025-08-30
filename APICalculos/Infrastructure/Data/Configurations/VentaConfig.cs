using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class VentaConfig : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.HasKey(v => v.VentaId);

            // Relación con Cliente: Restrict para no perder ventas históricas
            builder.HasOne(v => v.Client)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con TipoDePago: Restrict
            builder.HasOne(v => v.PaymentType)
                .WithMany()
                .HasForeignKey(v => v.TipoDePagoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con DetalleVenta: Cascade (si se borra una venta, se borran sus detalles)
            builder.HasMany(v => v.Detalle)
                .WithOne(dv => dv.Venta)
                .HasForeignKey(dv => dv.VentaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
