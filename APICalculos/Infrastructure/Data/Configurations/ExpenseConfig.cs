using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasOne(e => e.ExpenseType)
                .WithMany(et => et.Expenses)
                .HasForeignKey(e => e.ExpenseTypeId); 

            builder.Property(prop => prop.Description)
                .HasMaxLength(300);



        }
    }
}
