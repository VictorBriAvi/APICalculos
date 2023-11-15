using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class AgregandoCategoriasServicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreServicio",
                table: "TipoDeServicios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasServiciosId",
                table: "TipoDeServicios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioServicio",
                table: "TipoDeServicios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioServicioAumento",
                table: "TipoDeServicios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CategoriasServicios",
                columns: table => new
                {
                    CategoriasServiciosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCategoriaServicio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasServicios", x => x.CategoriasServiciosId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoDeServicios_CategoriasServiciosId",
                table: "TipoDeServicios",
                column: "CategoriasServiciosId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoDeServicios_CategoriasServicios_CategoriasServiciosId",
                table: "TipoDeServicios",
                column: "CategoriasServiciosId",
                principalTable: "CategoriasServicios",
                principalColumn: "CategoriasServiciosId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoDeServicios_CategoriasServicios_CategoriasServiciosId",
                table: "TipoDeServicios");

            migrationBuilder.DropTable(
                name: "CategoriasServicios");

            migrationBuilder.DropIndex(
                name: "IX_TipoDeServicios_CategoriasServiciosId",
                table: "TipoDeServicios");

            migrationBuilder.DropColumn(
                name: "CategoriasServiciosId",
                table: "TipoDeServicios");

            migrationBuilder.DropColumn(
                name: "PrecioServicio",
                table: "TipoDeServicios");

            migrationBuilder.DropColumn(
                name: "PrecioServicioAumento",
                table: "TipoDeServicios");

            migrationBuilder.AlterColumn<string>(
                name: "NombreServicio",
                table: "TipoDeServicios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
