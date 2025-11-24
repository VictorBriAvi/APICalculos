using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(v => v.Id);

            builder.HasOne(v => v.Client)
                .WithMany(c => c.Sale)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.SaleDetail)
                .WithOne(dv => dv.Sale)
                .HasForeignKey(dv => dv.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Payments)
                .WithOne(p => p.Sale)
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(v => v.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(v => v.DateSale)
                .IsRequired();

            builder.Property(v => v.IsDeleted)
                .HasDefaultValue(false);
        }
    }

}
