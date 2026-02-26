using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ServiceTypeConfig : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {

            builder.HasOne(st => st.ServiceCategories)
                   .WithMany(sc => sc.ServiceTypes)   // propiedad de navegación en ServiceCategorie
                   .HasForeignKey(st => st.ServiceCategorieId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(prop => prop.Name)
                   .HasMaxLength(100);


            builder.HasOne(e => e.Store)
    .WithMany()
    .HasForeignKey(e => e.StoreId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.StoreId);
        }
    }
}
