using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(c => c.Sale)
            .WithOne(v => v.Client)
            .HasForeignKey(v => v.ClientId);

            builder.HasMany(c => c.CustomerHistories)
            .WithOne(h => h.Client)
            .HasForeignKey(h => h.ClientId);

            builder.Property(prop => prop.Name)
                .HasMaxLength(300)
                .IsRequired();

        }
    }
}
