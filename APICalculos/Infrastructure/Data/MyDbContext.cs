using APICalculos.Domain.Entidades;
using APICalculos.Domain.Entities;
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

            modelBuilder.Entity<Sale>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<SaleDetail>().HasQueryFilter(d => !d.IsDeleted);
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CustomerHistory> CustomerHistories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<PaymentTypes> PaymentTypes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ServiceCategorie> ServiceCategories { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail>  SaleDetails { get; set; }
        public DbSet<SalePayment> SalePayments { get; set; }
    }
}
