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
        }
    }
}
