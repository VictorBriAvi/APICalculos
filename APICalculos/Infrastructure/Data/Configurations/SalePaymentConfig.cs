using APICalculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class SalePaymentConfig : IEntityTypeConfiguration<SalePayment>
    {
        public void Configure(EntityTypeBuilder<SalePayment> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.AmountPaid)
                .HasColumnType("decimal(18,2)").IsRequired();

            // Descuento que cobra la app al negocio (ej: 3% débito)
            builder.Property(sp => sp.AppDiscountPercent)
                .HasColumnType("decimal(5,2)").IsRequired();

            builder.Property(sp => sp.AppDiscountAmount)
                .HasColumnType("decimal(18,2)").IsRequired();

            // Lo que el negocio realmente recibe después del descuento de la app
            builder.Property(sp => sp.NetAmountReceived)
                .HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(sp => sp.PaymentDate)
                .HasColumnType("date").IsRequired();

            builder.HasOne(sp => sp.Sale)
                .WithMany(s => s.Payments)
                .HasForeignKey(sp => sp.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.PaymentType)
                .WithMany(pt => pt.SalePayments)
                .HasForeignKey(sp => sp.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sp => sp.Store)
                .WithMany()
                .HasForeignKey(sp => sp.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(sp => sp.StoreId);
        }
    }
}
