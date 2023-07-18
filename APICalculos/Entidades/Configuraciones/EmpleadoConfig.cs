using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Entidades.Configuraciones
{
    public class EmpleadoConfig : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.Property(prop => prop.NombreCompletoEmpleado)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(prop => prop.DocumentoNacional)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
