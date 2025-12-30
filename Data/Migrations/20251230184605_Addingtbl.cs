using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiskItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addingtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblRecipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeTime = table.Column<double>(type: "float", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRecipe", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(50)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    YearsOfExp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tblUserRecipeMapping",
                columns: table => new
                {
                    UserRecipeMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserRecipeMapping", x => x.UserRecipeMappingId);
                    table.ForeignKey(
                        name: "FK_tblUserRecipeMapping_tblRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "tblRecipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserRecipeMapping_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRecipeMapping_RecipeId",
                table: "tblUserRecipeMapping",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRecipeMapping_UserId",
                table: "tblUserRecipeMapping",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserRecipeMapping");

            migrationBuilder.DropTable(
                name: "tblRecipe");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
