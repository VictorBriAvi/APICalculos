using APICalculos.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APICalculos
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
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<HistorialClientes> HistorialClientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<TipoDePago> TipoDePagos { get; set; }
        public DbSet<TipoDeServicio> TipoDeServicios { get; set; }
        public DbSet<CategoriasServicios> CategoriasServicios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<TiposDeGastos> TiposDeGastos { get; set; }
  
        


    }
}
