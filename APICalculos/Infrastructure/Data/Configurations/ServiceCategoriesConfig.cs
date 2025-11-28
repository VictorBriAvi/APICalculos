using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ServiceCategoriesConfig : IEntityTypeConfiguration<ServiceCategorie>
    {
        public void Configure(EntityTypeBuilder<ServiceCategorie> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(prop => prop.Name)
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}
