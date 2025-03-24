using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyForAddToCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AddToCart_productID",
                table: "AddToCart",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK_AddToCart_Products_productID",
                table: "AddToCart",
                column: "productID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddToCart_Products_productID",
                table: "AddToCart");

            migrationBuilder.DropIndex(
                name: "IX_AddToCart_productID",
                table: "AddToCart");
        }
    }
}
