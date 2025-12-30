using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiskItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addingfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipeTime",
                table: "tblRecipe",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "RecipeDescription",
                table: "tblRecipe",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "tblUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "tblRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "tblRecipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "tblRecipe",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Calories",
                table: "tblRecipe");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "tblRecipe");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "tblRecipe");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "tblRecipe",
                newName: "RecipeTime");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tblRecipe",
                newName: "RecipeDescription");
        }
    }
}
