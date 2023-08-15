using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Dessert", "Test", "https://netms.blob.core.windows.net/mango/anna-tukhfatullina-food-photographer-stylist-Mzy-OjtCI70-unsplash.jpg", "Test", 10.99 },
                    { 2, "Appetizer", "Test", "https://netms.blob.core.windows.net/mango/anh-nguyen-kcA-c3f_3FE-unsplash.jpg", "tikki takka", 13.99 },
                    { 3, "Entree", "Test", "https://netms.blob.core.windows.net/mango/eiliv-aceron-ZuIDLSz3XLg-unsplash.jpg", "Rajin bari", 15.0 },
                    { 4, "Yummy", "Test test", "https://netms.blob.core.windows.net/mango/anna-pelzer-IGfIGP5ONV0-unsplash.jpg", "Delicioza", 12.199 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
