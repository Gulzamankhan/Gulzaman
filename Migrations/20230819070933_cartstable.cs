using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class cartstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingCarts_ShopingCartId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Users_AppUserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShopingCartId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_AppUserId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShopingCartId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "Carts");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShoppingCarts",
                newName: "AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "ShopingCartId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryTime",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopingCartId",
                table: "Products",
                column: "ShopingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_AppUserId",
                table: "ShoppingCarts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingCarts_ShopingCartId",
                table: "Products",
                column: "ShopingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Users_AppUserId",
                table: "ShoppingCarts",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
