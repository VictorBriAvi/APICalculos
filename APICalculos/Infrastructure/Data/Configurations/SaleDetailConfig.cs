using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class SaleDetailConfig : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(dv => dv.Id);

            // Relación con Venta: Cascade, borra los detalles si se borra la venta
            builder.HasOne(dv => dv.Sale)
                .WithMany(v => v.SaleDetail)
                .HasForeignKey(dv => dv.SaleId)
                .OnDelete(DeleteBehavior.Cascade);


            // Relación con TipoDeServicio: Restrict, no se puede borrar un servicio usado
            builder.HasOne(dv => dv.ServiceType)
                .WithMany(ts => ts.SaleDetail)
                .HasForeignKey(dv => dv.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Employee: Restrict, no se borran ventas históricas si se borra un empleado
            builder.HasOne(dv => dv.Employee)
                .WithMany(e => e.SaleDetail)
                .HasForeignKey(dv => dv.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(e => e.Store)
    .WithMany()
    .HasForeignKey(e => e.StoreId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.StoreId);
        }
    }
}
