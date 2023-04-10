using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductRatesCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ProductRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_CustomerId",
                table: "ProductRates",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRates_Customers_CustomerId",
                table: "ProductRates",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRates_Customers_CustomerId",
                table: "ProductRates");

            migrationBuilder.DropIndex(
                name: "IX_ProductRates_CustomerId",
                table: "ProductRates");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ProductRates");
        }
    }
}
