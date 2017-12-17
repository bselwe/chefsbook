using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChefsBook.MigrationsApp.Migrations.CoreDb
{
    public partial class AddCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "core",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RecipeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => new { x.UserId, x.RecipeId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "core");
        }
    }
}
