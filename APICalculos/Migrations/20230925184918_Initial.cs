using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposDeGastos",
                columns: table => new
                {
                    TipoDeGastosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipoDeGastos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeGastos", x => x.TipoDeGastosId);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    GastosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGastos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionGastos = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PrecioGasto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaGastos = table.Column<DateTime>(type: "date", nullable: false),
                    TipoDeGastosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.GastosId);
                    table.ForeignKey(
                        name: "FK_Gastos_TiposDeGastos_TipoDeGastosId",
                        column: x => x.TipoDeGastosId,
                        principalTable: "TiposDeGastos",
                        principalColumn: "TipoDeGastosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_TipoDeGastosId",
                table: "Gastos",
                column: "TipoDeGastosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "TiposDeGastos");
        }
    }
}
