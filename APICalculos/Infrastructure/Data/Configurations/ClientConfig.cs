using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ClientConfig : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> builder)
        {
            builder.HasMany(c => c.Ventas)
            .WithOne(v => v.Client)
            .HasForeignKey(v => v.ClienteId);

            builder.HasMany(c => c.CustomerHistories)
            .WithOne(h => h.Client)
            .HasForeignKey(h => h.ClientId);

            builder.Property(prop => prop.Name)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(prop => prop.IdentityDocument)
                .HasMaxLength(20)
                .IsRequired();


        }
    }
}
