using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMS.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Lots",
                newName: "PurchaseDetailId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "Lots",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lots",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Lots",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Lots",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    PhoneNumberId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.PhoneNumberId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    VariantId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderQuantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    VatPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    VatAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.PurchaseDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierName = table.Column<string>(type: "text", nullable: true),
                    SupplierCode = table.Column<string>(type: "text", nullable: true),
                    SupplierEmail = table.Column<string>(type: "text", nullable: true),
                    SupplierAddress = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lots_PurchaseDetailId",
                table: "Lots",
                column: "PurchaseDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_SupplierId",
                table: "Lots",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_PurchaseDetail_PurchaseDetailId",
                table: "Lots",
                column: "PurchaseDetailId",
                principalTable: "PurchaseDetail",
                principalColumn: "PurchaseDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Suppliers_SupplierId",
                table: "Lots",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_PurchaseDetail_PurchaseDetailId",
                table: "Lots");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Suppliers_SupplierId",
                table: "Lots");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Lots_PurchaseDetailId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_SupplierId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "PurchaseDetailId",
                table: "Lots",
                newName: "PurchaseId");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Lots",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
