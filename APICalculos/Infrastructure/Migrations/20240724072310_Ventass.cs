using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class Ventass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriasServiciosId1",
                table: "TipoDeServicios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    VentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TipoDePagoId = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.VentaId);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_TipoDePagos_TipoDePagoId",
                        column: x => x.TipoDePagoId,
                        principalTable: "TipoDePagos",
                        principalColumn: "TipoDePagoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    DetalleVentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    TipoDeServicioId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.DetalleVentaId);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_TipoDeServicios_TipoDeServicioId",
                        column: x => x.TipoDeServicioId,
                        principalTable: "TipoDeServicios",
                        principalColumn: "TipoDeServicioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "VentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoDeServicios_CategoriasServiciosId1",
                table: "TipoDeServicios",
                column: "CategoriasServiciosId1");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_TipoDeServicioId",
                table: "DetalleVentas",
                column: "TipoDeServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_TipoDePagoId",
                table: "Ventas",
                column: "TipoDePagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoDeServicios_CategoriasServicios_CategoriasServiciosId1",
                table: "TipoDeServicios",
                column: "CategoriasServiciosId1",
                principalTable: "CategoriasServicios",
                principalColumn: "CategoriasServiciosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoDeServicios_CategoriasServicios_CategoriasServiciosId1",
                table: "TipoDeServicios");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_TipoDeServicios_CategoriasServiciosId1",
                table: "TipoDeServicios");

            migrationBuilder.DropColumn(
                name: "CategoriasServiciosId1",
                table: "TipoDeServicios");
        }
    }
}
