using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChefsBook.MigrationsApp.Migrations
{
    public partial class AddTagsDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Tag_TagId",
                schema: "core",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Recipes_RecipeId",
                schema: "core",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                schema: "core",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTag",
                schema: "core",
                table: "RecipeTag");

            migrationBuilder.RenameTable(
                name: "Tag",
                schema: "core",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "RecipeTag",
                schema: "core",
                newName: "RecipeTags");

            migrationBuilder.RenameIndex(
                name: "IX_Tag_RecipeId",
                schema: "core",
                table: "Tags",
                newName: "IX_Tags_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                schema: "core",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTags",
                schema: "core",
                table: "RecipeTags",
                columns: new[] { "TagId", "RecipeId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                schema: "core",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Recipes_RecipeId",
                schema: "core",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                schema: "core",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTags",
                schema: "core",
                table: "RecipeTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                schema: "core",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "RecipeTags",
                schema: "core",
                newName: "RecipeTag");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_RecipeId",
                schema: "core",
                table: "Tag",
                newName: "IX_Tag_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                schema: "core",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTag",
                schema: "core",
                table: "RecipeTag",
                columns: new[] { "TagId", "RecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Tag_TagId",
                schema: "core",
                table: "RecipeTag",
                column: "TagId",
                principalSchema: "core",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Recipes_RecipeId",
                schema: "core",
                table: "Tag",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
