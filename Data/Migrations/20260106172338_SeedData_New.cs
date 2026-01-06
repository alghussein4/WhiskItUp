using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiskItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "food",
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

            migrationBuilder.InsertData(
                schema: "food",
                table: "tblRecipe",
                columns: new[] { "RecipeId", "Calories", "Description", "Difficulty", "IsVegetarian", "RecipeName", "Servings", "Time" },
                values: new object[,]
                {
                    { 101, 300, "Fluffy banana breakfast pancakes", 0, true, "Banana Pancakes", 2, 25.0 },
                    { 102, 600, "Creamy spaghetti with bacon", 1, false, "Spaghetti Carbonara", 4, 40.0 },
                    { 103, 180, "Fresh and healthy tomato soup", 0, true, "Tomato Soup", 3, 35.0 },
                    { 104, 500, "Tangy lemon dessert", 2, true, "Lemon Cheesecake", 6, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "UserId", "Email", "FullName", "Gender", "PhoneNumber", "YearsOfExp" },
                values: new object[,]
                {
                    { 101, "clara@example.com", "Clara Evans", 0, "5551112222", new DateTime(2016, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, "david@example.com", "David Miller", 1, "5553334444", new DateTime(2013, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "tblUserRecipeMapping",
                columns: new[] { "UserRecipeMappingId", "Comment", "Favorite", "Rating", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 101, "Perfect banana pancakes!", true, 5.0, 101, 101 },
                    { 102, "Tomato soup is very tasty.", false, 4.0, 103, 101 },
                    { 103, "Carbonara was amazing.", true, 4.5, 102, 102 },
                    { 104, "Lemon cheesecake is delicious.", false, 5.0, 104, 102 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "tblUserRecipeMapping",
                keyColumn: "UserRecipeMappingId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "food",
                table: "tblRecipe",
                keyColumn: "RecipeId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "UserId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "UserId",
                keyValue: 102);

            migrationBuilder.InsertData(
                schema: "food",
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
    }
}
