using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class AddStoreToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRoles_Usuarios_UsuarioId",
                table: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ServiceTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ServiceCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "SalePayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "SaleDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PaymentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ExpenseTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "CustomerHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityDocument",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateOn = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_StoreId",
                table: "ServiceTypes",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategories_StoreId",
                table: "ServiceCategories",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StoreId",
                table: "Sales",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayments_StoreId",
                table: "SalePayments",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_StoreId",
                table: "SaleDetails",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_StoreId",
                table: "Productos",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_StoreId",
                table: "PaymentTypes",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_StoreId",
                table: "ExpenseTypes",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_StoreId",
                table: "Expenses",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerHistories_StoreId",
                table: "CustomerHistories",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StoreId",
                table: "Clients",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StoreId",
                table: "Users",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Store_StoreId",
                table: "Clients",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerHistories_Store_StoreId",
                table: "CustomerHistories",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Store_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Store_StoreId",
                table: "Expenses",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTypes_Store_StoreId",
                table: "ExpenseTypes",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTypes_Store_StoreId",
                table: "PaymentTypes",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Store_StoreId",
                table: "Productos",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Store_StoreId",
                table: "SaleDetails",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalePayments_Store_StoreId",
                table: "SalePayments",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Store_StoreId",
                table: "Sales",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategories_Store_StoreId",
                table: "ServiceCategories",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Store_StoreId",
                table: "ServiceTypes",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRoles_Users_UsuarioId",
                table: "UsuarioRoles",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Store_StoreId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerHistories_Store_StoreId",
                table: "CustomerHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Store_StoreId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Store_StoreId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTypes_Store_StoreId",
                table: "ExpenseTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTypes_Store_StoreId",
                table: "PaymentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Store_StoreId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Store_StoreId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalePayments_Store_StoreId",
                table: "SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Store_StoreId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategories_Store_StoreId",
                table: "ServiceCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Store_StoreId",
                table: "ServiceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRoles_Users_UsuarioId",
                table: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_StoreId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCategories_StoreId",
                table: "ServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_Sales_StoreId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SalePayments_StoreId",
                table: "SalePayments");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_StoreId",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_Productos_StoreId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTypes_StoreId",
                table: "PaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseTypes_StoreId",
                table: "ExpenseTypes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_StoreId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Employees_StoreId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_CustomerHistories_StoreId",
                table: "CustomerHistories");

            migrationBuilder.DropIndex(
                name: "IX_Clients_StoreId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ServiceCategories");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "SalePayments");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "SaleDetails");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ExpenseTypes");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CustomerHistories");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityDocument",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreCompletoUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreDeUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRoles_Usuarios_UsuarioId",
                table: "UsuarioRoles",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
