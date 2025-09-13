using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasOne(ur => ur.ExpenseTypes)
                .WithMany(g => g.Expense)
                .HasForeignKey(g => g.Id);

            builder.Property(prop => prop.Description)
                .HasMaxLength(300);


        }               
    }
}
