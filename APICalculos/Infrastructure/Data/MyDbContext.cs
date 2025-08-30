using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APICalculos.Infrastructure.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<HistorialClientes> HistorialClientes { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ServiceCategorie> ServiceCategories { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta>  DetalleVentas { get; set; }
    }
}
