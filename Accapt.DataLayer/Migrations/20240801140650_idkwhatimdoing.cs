using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accapt.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class idkwhatimdoing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Users_UserId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatrgory_Product_ProductId",
                table: "ProductCatrgory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatrgory",
                table: "ProductCatrgory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductCatrgory",
                newName: "ProductCatrgories");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "products");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatrgory_ProductId",
                table: "ProductCatrgories",
                newName: "IX_ProductCatrgories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserId",
                table: "products",
                newName: "IX_products_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatrgories",
                table: "ProductCatrgories",
                column: "CatrgoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatrgories_products_ProductId",
                table: "ProductCatrgories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Users_UserId",
                table: "products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatrgories_products_ProductId",
                table: "ProductCatrgories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Users_UserId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatrgories",
                table: "ProductCatrgories");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductCatrgories",
                newName: "ProductCatrgory");

            migrationBuilder.RenameIndex(
                name: "IX_products_UserId",
                table: "Product",
                newName: "IX_Product_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatrgories_ProductId",
                table: "ProductCatrgory",
                newName: "IX_ProductCatrgory_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatrgory",
                table: "ProductCatrgory",
                column: "CatrgoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Users_UserId",
                table: "Product",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatrgory_Product_ProductId",
                table: "ProductCatrgory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}
