using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class PaymentTypeConfig : IEntityTypeConfiguration<PaymentTypes>
    {
        public void Configure(EntityTypeBuilder<PaymentTypes> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name)
                .HasMaxLength(100)
                .IsRequired();


            builder.HasMany(pt => pt.SalePayments)
                .WithOne(sp => sp.PaymentType)
                .HasForeignKey(sp => sp.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
