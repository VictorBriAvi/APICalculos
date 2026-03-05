using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class AddPaymentTypeDiscountSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AppliedDiscountPercent",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "SalePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ApplyDiscount",
                table: "PaymentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "PaymentTypes",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDiscountPercent",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "ApplyDiscount",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "PaymentTypes");
        }
    }
}
