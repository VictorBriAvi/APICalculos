using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class HistorialClienteRenovado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Historial",
                table: "Clientes");

            migrationBuilder.CreateTable(
                name: "HistorialClientes",
                columns: table => new
                {
                    HistorialClientesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDeHistorialCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionHistorialCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaHistorial = table.Column<DateTime>(type: "date", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialClientes", x => x.HistorialClientesId);
                    table.ForeignKey(
                        name: "FK_HistorialClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialClientes_ClienteId",
                table: "HistorialClientes",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialClientes");

            migrationBuilder.AddColumn<string>(
                name: "Historial",
                table: "Clientes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
