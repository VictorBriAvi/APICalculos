using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class AgregandoFechaServiio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaIngresoServicio",
                table: "Servicios",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaIngresoServicio",
                table: "Servicios");
        }
    }
}
