using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasMany(c => c.Ventas)
            .WithOne(v => v.Cliente)
            .HasForeignKey(v => v.ClienteId);

            builder.HasMany(c => c.HistorialClientes)
            .WithOne(h => h.Cliente)
            .HasForeignKey(h => h.ClienteId);

            builder.Property(prop => prop.NombreCompletoCliente)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(prop => prop.NumeroDocumento)
                .HasMaxLength(20)
                .IsRequired();


        }
    }
}
