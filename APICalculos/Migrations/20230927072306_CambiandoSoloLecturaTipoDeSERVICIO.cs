using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class CambiandoSoloLecturaTipoDeSERVICIO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioServicioAumento",
                table: "TipoDeServicios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioServicioAumento",
                table: "TipoDeServicios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
