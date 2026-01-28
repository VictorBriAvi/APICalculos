using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class AddPaymentTypeToExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentTypeId",
                table: "Expenses",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PaymentTypes_PaymentTypeId",
                table: "Expenses",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PaymentTypes_PaymentTypeId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaymentTypeId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Expenses");
        }
    }
}
