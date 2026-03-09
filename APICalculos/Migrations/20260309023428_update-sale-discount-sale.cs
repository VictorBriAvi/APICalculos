using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class updatesalediscountsale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDiscountPercent",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "SalePayments");

            migrationBuilder.RenameColumn(
                name: "SurchargeAmount",
                table: "SalePayments",
                newName: "NetAmountReceived");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "SalePayments",
                newName: "AppDiscountAmount");

            migrationBuilder.RenameColumn(
                name: "AppliedSurchargePercent",
                table: "SalePayments",
                newName: "AppDiscountPercent");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseAmount",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SurchargeAmount",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SurchargePercent",
                table: "Sales",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "BaseAmount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SurchargeAmount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SurchargePercent",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "NetAmountReceived",
                table: "SalePayments",
                newName: "SurchargeAmount");

            migrationBuilder.RenameColumn(
                name: "AppDiscountPercent",
                table: "SalePayments",
                newName: "AppliedSurchargePercent");

            migrationBuilder.RenameColumn(
                name: "AppDiscountAmount",
                table: "SalePayments",
                newName: "NetAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedDiscountPercent",
                table: "SalePayments",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalAmount",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
