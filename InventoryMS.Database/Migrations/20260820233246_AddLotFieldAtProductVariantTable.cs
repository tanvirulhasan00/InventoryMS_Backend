using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMS.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddLotFieldAtProductVariantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LotId",
                table: "ProductVariants",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    LotId = table.Column<Guid>(type: "uuid", nullable: false),
                    LotNumber = table.Column<int>(type: "integer", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.LotId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_LotId",
                table: "ProductVariants",
                column: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Lots_LotId",
                table: "ProductVariants",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "LotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Lots_LotId",
                table: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_LotId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "LotId",
                table: "ProductVariants");
        }
    }
}
