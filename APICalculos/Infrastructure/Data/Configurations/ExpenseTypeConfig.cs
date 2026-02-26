using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ExpenseTypeConfig : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(e => e.Store)
    .WithMany()
    .HasForeignKey(e => e.StoreId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.StoreId);
        }
    }
}
