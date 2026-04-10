using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToMealNameColumnForMealEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Meals_Name",
                schema: "Menu",
                table: "Meals",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meals_Name",
                schema: "Menu",
                table: "Meals");
        }
    }
}
