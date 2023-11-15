﻿// <auto-generated />
using System;
using APICalculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APICalculos.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230927072306_CambiandoSoloLecturaTipoDeSERVICIO")]
    partial class CambiandoSoloLecturaTipoDeSERVICIO
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("APICalculos.Entidades.CategoriasServicios", b =>
                {
                    b.Property<int>("CategoriasServiciosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriasServiciosId"), 1L, 1);

                    b.Property<string>("NombreCategoriaServicio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoriasServiciosId");

                    b.ToTable("CategoriasServicios");
                });

            modelBuilder.Entity("APICalculos.Entidades.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"), 1L, 1);

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Historial")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("NombreCompletoCliente")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("NumeroDocumento")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("APICalculos.Entidades.Empleado", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpleadoId"), 1L, 1);

                    b.Property<string>("DocumentoNacional")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("NombreCompletoEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("EmpleadoId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("APICalculos.Entidades.Gastos", b =>
                {
                    b.Property<int>("GastosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GastosId"), 1L, 1);

                    b.Property<string>("DescripcionGastos")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("FechaGastos")
                        .HasColumnType("date");

                    b.Property<decimal>("PrecioGasto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoDeGastosId")
                        .HasColumnType("int");

                    b.HasKey("GastosId");

                    b.HasIndex("TipoDeGastosId");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("APICalculos.Entidades.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"), 1L, 1);

                    b.Property<string>("CodigoProducto")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("DescripcionProducto")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PrecioProducto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("APICalculos.Entidades.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolId"), 1L, 1);

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RolId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("APICalculos.Entidades.Servicio", b =>
                {
                    b.Property<int>("ServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServicioId"), 1L, 1);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaIngresoServicio")
                        .HasColumnType("date");

                    b.Property<int>("TipoDePagoId")
                        .HasColumnType("int");

                    b.Property<int>("TipoDeServicioId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorServicio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ServicioId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("TipoDePagoId");

                    b.HasIndex("TipoDeServicioId");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("APICalculos.Entidades.TipoDePago", b =>
                {
                    b.Property<int>("TipoDePagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoDePagoId"), 1L, 1);

                    b.Property<string>("NombreTipoDePago")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoDePagoId");

                    b.ToTable("TipoDePagos");
                });

            modelBuilder.Entity("APICalculos.Entidades.TipoDeServicio", b =>
                {
                    b.Property<int>("TipoDeServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoDeServicioId"), 1L, 1);

                    b.Property<int>("CategoriasServiciosId")
                        .HasColumnType("int");

                    b.Property<string>("NombreServicio")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PrecioServicio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TipoDeServicioId");

                    b.HasIndex("CategoriasServiciosId");

                    b.ToTable("TipoDeServicios");
                });

            modelBuilder.Entity("APICalculos.Entidades.TiposDeGastos", b =>
                {
                    b.Property<int>("TipoDeGastosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoDeGastosId"), 1L, 1);

                    b.Property<string>("NombreTipoDeGastos")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoDeGastosId");

                    b.ToTable("TiposDeGastos");
                });

            modelBuilder.Entity("APICalculos.Entidades.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"), 1L, 1);

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreCompletoUsuario")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreDeUsuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("APICalculos.Entidades.UsuarioRol", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "RolId");

                    b.HasIndex("RolId");

                    b.ToTable("UsuarioRoles");
                });

            modelBuilder.Entity("APICalculos.Entidades.Gastos", b =>
                {
                    b.HasOne("APICalculos.Entidades.TiposDeGastos", "TiposDeGastos")
                        .WithMany("Gastos")
                        .HasForeignKey("TipoDeGastosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TiposDeGastos");
                });

            modelBuilder.Entity("APICalculos.Entidades.Servicio", b =>
                {
                    b.HasOne("APICalculos.Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICalculos.Entidades.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICalculos.Entidades.TipoDePago", "TipoDePago")
                        .WithMany()
                        .HasForeignKey("TipoDePagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICalculos.Entidades.TipoDeServicio", "TipoDeServicio")
                        .WithMany()
                        .HasForeignKey("TipoDeServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Empleado");

                    b.Navigation("TipoDePago");

                    b.Navigation("TipoDeServicio");
                });

            modelBuilder.Entity("APICalculos.Entidades.TipoDeServicio", b =>
                {
                    b.HasOne("APICalculos.Entidades.CategoriasServicios", "CategoriasServicios")
                        .WithMany("TipoDeServicios")
                        .HasForeignKey("CategoriasServiciosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriasServicios");
                });

            modelBuilder.Entity("APICalculos.Entidades.UsuarioRol", b =>
                {
                    b.HasOne("APICalculos.Entidades.Rol", "Rol")
                        .WithMany("RolesUsuario")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICalculos.Entidades.Usuario", "Usuario")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("APICalculos.Entidades.CategoriasServicios", b =>
                {
                    b.Navigation("TipoDeServicios");
                });

            modelBuilder.Entity("APICalculos.Entidades.Rol", b =>
                {
                    b.Navigation("RolesUsuario");
                });

            modelBuilder.Entity("APICalculos.Entidades.TiposDeGastos", b =>
                {
                    b.Navigation("Gastos");
                });

            modelBuilder.Entity("APICalculos.Entidades.Usuario", b =>
                {
                    b.Navigation("UsuarioRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
