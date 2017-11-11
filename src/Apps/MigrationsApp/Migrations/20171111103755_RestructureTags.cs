using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChefsBook.MigrationsApp.Migrations
{
    public partial class RestructureTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                schema: "core",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Recipes_RecipeId",
                schema: "core",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_RecipeId",
                schema: "core",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                schema: "core",
                table: "Tags");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTags_RecipeId",
                schema: "core",
                table: "RecipeTags",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                schema: "core",
                table: "RecipeTags",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                schema: "core",
                table: "RecipeTags");

            migrationBuilder.DropIndex(
                name: "IX_RecipeTags_RecipeId",
                schema: "core",
                table: "RecipeTags");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                schema: "core",
                table: "Tags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RecipeId",
                schema: "core",
                table: "Tags",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                schema: "core",
                table: "RecipeTags",
                column: "TagId",
                principalSchema: "core",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Recipes_RecipeId",
                schema: "core",
                table: "Tags",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
