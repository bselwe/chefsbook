using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChefsBook.MigrationsApp.Migrations
{
    public partial class CreateRecipeTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipes_RecipeId",
                schema: "core",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Recipes_RecipeId",
                schema: "core",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Step",
                schema: "core",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                schema: "core",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Step",
                schema: "core",
                newName: "Steps");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                schema: "core",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Step_RecipeId",
                schema: "core",
                table: "Steps",
                newName: "IX_Steps_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_RecipeId",
                schema: "core",
                table: "Ingredients",
                newName: "IX_Ingredients_RecipeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "core",
                table: "Tag",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Steps",
                schema: "core",
                table: "Steps",
                columns: new[] { "StepId", "RecipeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                schema: "core",
                table: "Ingredients",
                columns: new[] { "IngredientId", "RecipeId" });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                schema: "core",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.TagId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "core",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                schema: "core",
                table: "Ingredients",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Recipes_RecipeId",
                schema: "core",
                table: "Steps",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                schema: "core",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Recipes_RecipeId",
                schema: "core",
                table: "Steps");

            migrationBuilder.DropTable(
                name: "RecipeTag",
                schema: "core");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Steps",
                schema: "core",
                table: "Steps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                schema: "core",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Steps",
                schema: "core",
                newName: "Step");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                schema: "core",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_RecipeId",
                schema: "core",
                table: "Step",
                newName: "IX_Step_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_RecipeId",
                schema: "core",
                table: "Ingredient",
                newName: "IX_Ingredient_RecipeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "core",
                table: "Tag",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Step",
                schema: "core",
                table: "Step",
                columns: new[] { "StepId", "RecipeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                schema: "core",
                table: "Ingredient",
                columns: new[] { "IngredientId", "RecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipes_RecipeId",
                schema: "core",
                table: "Ingredient",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Recipes_RecipeId",
                schema: "core",
                table: "Step",
                column: "RecipeId",
                principalSchema: "core",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
