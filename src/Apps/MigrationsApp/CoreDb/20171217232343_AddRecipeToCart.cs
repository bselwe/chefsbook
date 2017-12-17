using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChefsBook.MigrationsApp.Migrations.CoreDb
{
    public partial class AddRecipeToCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartItems_RecipeId",
                schema: "core",
                table: "CartItems",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Recipes_RecipeId",
                schema: "core",
                table: "CartItems",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Recipes_RecipeId",
                schema: "core",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_RecipeId",
                schema: "core",
                table: "CartItems");
        }
    }
}
