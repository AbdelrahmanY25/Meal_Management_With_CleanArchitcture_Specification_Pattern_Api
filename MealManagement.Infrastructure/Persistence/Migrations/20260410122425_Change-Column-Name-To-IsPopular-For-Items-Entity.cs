using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameToIsPopularForItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPobular",
                schema: "Menu",
                table: "OptionGroupItems",
                newName: "IsPopular");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPopular",
                schema: "Menu",
                table: "OptionGroupItems",
                newName: "IsPobular");
        }
    }
}
