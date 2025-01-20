using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class Actualizacionventa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "DetalleVentas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_EmpleadoId",
                table: "DetalleVentas",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Empleados_EmpleadoId",
                table: "DetalleVentas",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Empleados_EmpleadoId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_EmpleadoId",
                table: "DetalleVentas");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "DetalleVentas");
        }
    }
}
