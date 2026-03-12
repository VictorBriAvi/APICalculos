using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.BaseAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(s => s.SurchargePercent).HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(s => s.SurchargeAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(s => s.DateSale).HasColumnType("date").IsRequired();
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
            builder.HasOne(s => s.Store).WithMany().HasForeignKey(s => s.StoreId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(s => s.StoreId);
        }
    }

}
