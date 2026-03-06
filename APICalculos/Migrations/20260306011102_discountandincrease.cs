using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class discountandincrease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AppliedDiscountPercent",
                table: "SalePayments",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedSurchargePercent",
                table: "SalePayments",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalAmount",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SurchargeAmount",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ApplySurcharge",
                table: "PaymentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SurchargePercent",
                table: "PaymentTypes",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedSurchargePercent",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "SurchargeAmount",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "ApplySurcharge",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "SurchargePercent",
                table: "PaymentTypes");

            migrationBuilder.AlterColumn<decimal>(
                name: "AppliedDiscountPercent",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }
    }
}
