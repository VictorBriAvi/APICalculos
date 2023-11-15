using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class TipoDeGastosConfig : IEntityTypeConfiguration<TiposDeGastos>
    {
        public void Configure(EntityTypeBuilder<TiposDeGastos> builder)
        {
            builder.HasKey(t => t.TipoDeGastosId);
        }
    }
}
