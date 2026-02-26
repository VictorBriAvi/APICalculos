using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class RolConfig : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }

}
