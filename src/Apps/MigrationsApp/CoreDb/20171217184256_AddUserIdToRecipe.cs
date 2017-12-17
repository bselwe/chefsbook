using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChefsBook.MigrationsApp.Migrations.CoreDb
{
    public partial class AddUserIdToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StepId",
                schema: "core",
                table: "Steps",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "core",
                table: "Recipes",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                schema: "core",
                table: "Ingredients",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "core",
                table: "Recipes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "core",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "core",
                table: "Steps",
                newName: "StepId");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                schema: "core",
                table: "Recipes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "core",
                table: "Ingredients",
                newName: "IngredientId");
        }
    }
}
