using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.Property(prop => prop.NombreProducto)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.DescripcionProducto)
                .HasMaxLength(300)
                .IsRequired();
            builder.Property(prop => prop.CodigoProducto)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(prop => prop.PrecioProducto)
                .HasColumnType("decimal(18,2)")
                .IsRequired();


        }
    }
}
