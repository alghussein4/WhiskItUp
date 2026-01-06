using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiskItUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeSchemaName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "food");

            migrationBuilder.RenameTable(
                name: "tblRecipe",
                newName: "tblRecipe",
                newSchema: "food");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tblRecipe",
                schema: "food",
                newName: "tblRecipe");
        }
    }
}
