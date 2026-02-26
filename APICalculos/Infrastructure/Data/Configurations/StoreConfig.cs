using APICalculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(s => s.CreateOn)
                .IsRequired();

            builder.HasMany(s => s.Users)
                .WithOne(u => u.Store)
                .HasForeignKey(u => u.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
