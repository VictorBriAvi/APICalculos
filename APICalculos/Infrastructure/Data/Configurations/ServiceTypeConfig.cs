using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ServiceTypeConfig : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {

            builder.HasOne(st => st.ServiceCategorie)
                   .WithMany(sc => sc.ServiceTypes)   // propiedad de navegación en ServiceCategorie
                   .HasForeignKey(st => st.ServiceCategorieId)
                   .OnDelete(DeleteBehavior.Restrict); // o la política que prefieras

            builder.Property(prop => prop.Name)
                   .HasMaxLength(100);

        }
    }
}
