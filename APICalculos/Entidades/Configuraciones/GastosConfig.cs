using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class GastosConfig : IEntityTypeConfiguration<Gastos>
    {
        public void Configure(EntityTypeBuilder<Gastos> builder)
        {
            builder.HasOne(ur => ur.TiposDeGastos)
                .WithMany(g => g.Gastos)
                .HasForeignKey(g => g.TipoDeGastosId);

            builder.Property(prop => prop.DescripcionGastos)
                .HasMaxLength(300);


        }               
    }
}
