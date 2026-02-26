using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICalculos.Migrations
{
    public partial class InitialStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Users_Store_StoreId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UsuarioRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Store",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NameUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "Store");

            migrationBuilder.RenameTable(
                name: "Store",
                newName: "Stores");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "NombreRol",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stores",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RolId",
                table: "UserRoles",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Stores_StoreId",
                table: "Clients",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerHistories_Stores_StoreId",
                table: "CustomerHistories",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Stores_StoreId",
                table: "Expenses",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTypes_Stores_StoreId",
                table: "ExpenseTypes",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTypes_Stores_StoreId",
                table: "PaymentTypes",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Stores_StoreId",
                table: "Productos",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Stores_StoreId",
                table: "SaleDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalePayments_Stores_StoreId",
                table: "SalePayments",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Stores_StoreId",
                table: "Sales",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategories_Stores_StoreId",
                table: "ServiceCategories",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Stores_StoreId",
                table: "ServiceTypes",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Stores_StoreId",
                table: "Users",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Stores_StoreId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerHistories_Stores_StoreId",
                table: "CustomerHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Stores_StoreId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTypes_Stores_StoreId",
                table: "ExpenseTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTypes_Stores_StoreId",
                table: "PaymentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Stores_StoreId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Stores_StoreId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalePayments_Stores_StoreId",
                table: "SalePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Stores_StoreId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategories_Stores_StoreId",
                table: "ServiceCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Stores_StoreId",
                table: "ServiceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Stores_StoreId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Stores",
                newName: "Store");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "NombreRol");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RolId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameUser",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Store",
                table: "Store",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RolId",
                table: "UsuarioRoles",
                column: "RolId");

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
                name: "FK_Users_Store_StoreId",
                table: "Users",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
