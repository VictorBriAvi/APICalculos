using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class VentaConfig : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.HasKey(v => v.VentaId);
            builder.HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId);
            builder.HasOne(v => v.TipoDePago)
                .WithMany()
                .HasForeignKey(v => v.TipoDePagoId);
            builder.HasMany(v => v.Detalle)
                .WithOne(dv => dv.Venta)
                .HasForeignKey(dv => dv.VentaId);
        }
    }
}
