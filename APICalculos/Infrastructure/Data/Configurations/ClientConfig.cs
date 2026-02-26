using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // 🔗 Client → Store
            builder.HasOne(c => c.Store)
                .WithMany() // Store no necesita ICollection<Client>
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Client → Sales
            builder.HasMany(c => c.Sale)
                .WithOne(v => v.Client)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Client → CustomerHistory
            builder.HasMany(c => c.CustomerHistories)
                .WithOne(h => h.Client)
                .HasForeignKey(h => h.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🧾 Propiedades
            builder.Property(prop => prop.Name)
                .HasMaxLength(300)
                .IsRequired();

            // 🚀 Performance
            builder.HasIndex(c => c.StoreId);
        }
    }

}
