using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHasOptionsFlagToMealsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasOptionGroup",
                table: "Meals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasOptionGroup",
                table: "Meals");
        }
    }
}
