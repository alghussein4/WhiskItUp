using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiskItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tblRecipe",
                columns: new[] { "RecipeId", "Calories", "Description", "Difficulty", "IsVegetarian", "RecipeName", "Servings", "Time" },
                values: new object[,]
                {
                    { 1, 350, "Fluffy breakfast pancakes", 0, true, "Pancakes", 4, 20.0 },
                    { 2, 650, "Creamy pasta with grilled chicken", 1, false, "Chicken Alfredo", 3, 45.0 },
                    { 3, 200, "Healthy mixed vegetable soup", 0, true, "Vegetable Soup", 5, 30.0 },
                    { 4, 450, "Rich chocolate dessert", 2, true, "Chocolate Cake", 8, 90.0 }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "UserId", "Email", "FullName", "Gender", "PhoneNumber", "YearsOfExp" },
                values: new object[,]
                {
                    { 1, "alice@example.com", "Alice Johnson Smith", 0, "1234567890", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "bob@example.com", "Bob Alexander Brown", 1, "0987654321", new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "tblUserRecipeMapping",
                columns: new[] { "UserRecipeMappingId", "Comment", "Favorite", "Rating", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 1, "Delicious and easy to make!", true, 5.0, 1, 1 },
                    { 2, "Healthy and tasty soup.", false, 4.0, 3, 1 },
                    { 3, "Creamy pasta, kids loved it.", true, 4.5, 2, 2 },
                    { 4, "Rich chocolate cake, perfect dessert.", false, 5.0, 4, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "UserId",
                keyValue: 2);
        }
    }
}
