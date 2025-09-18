using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(prop => prop.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(prop => prop.IdentityDocument)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
