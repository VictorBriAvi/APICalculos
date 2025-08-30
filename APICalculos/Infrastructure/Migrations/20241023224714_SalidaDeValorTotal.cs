using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class SalidaDeValorTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Ventas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Ventas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
