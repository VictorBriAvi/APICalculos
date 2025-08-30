using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ServicioConfig : IEntityTypeConfiguration<Servicio>
    {
   

        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.Property(prop => prop.ValorServicio)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
