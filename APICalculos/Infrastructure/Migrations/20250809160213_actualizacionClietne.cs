using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class actualizacionClietne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroDocumento",
                table: "Clientes",
                newName: "IdentityDocument");

            migrationBuilder.RenameColumn(
                name: "NombreCompletoCliente",
                table: "Clientes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Clientes",
                newName: "DateBirth");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Clientes",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clientes",
                newName: "NombreCompletoCliente");

            migrationBuilder.RenameColumn(
                name: "IdentityDocument",
                table: "Clientes",
                newName: "NumeroDocumento");

            migrationBuilder.RenameColumn(
                name: "DateBirth",
                table: "Clientes",
                newName: "FechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "ClienteId");
        }
    }
}
