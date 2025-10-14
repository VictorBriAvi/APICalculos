using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(v => v.Id);

            // Relación con Cliente: Restrict para no perder ventas históricas
            builder.HasOne(v => v.Client)
                .WithMany(c => c.Sale)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con TipoDePago: Restrict
            builder.HasOne(v => v.PaymentType)
                .WithMany()
                .HasForeignKey(v => v.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con DetalleVenta: Cascade (si se borra una venta, se borran sus detalles)
            builder.HasMany(v => v.SaleDetail)
                .WithOne(dv => dv.Sale)
                .HasForeignKey(dv => dv.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
