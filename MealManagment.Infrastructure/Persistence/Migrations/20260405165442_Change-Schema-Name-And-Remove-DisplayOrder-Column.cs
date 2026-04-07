using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagment.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemaNameAndRemoveDisplayOrderColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OptionGroupItems_OptionGroupId",
                table: "OptionGroupItems");

            migrationBuilder.DropIndex(
                name: "IX_MealOptionGroups_MealId",
                table: "MealOptionGroups");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "OptionGroupItems");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "MealOptionGroups");

            migrationBuilder.EnsureSchema(
                name: "Menu");

            migrationBuilder.RenameTable(
                name: "OptionGroupItems",
                newName: "OptionGroupItems",
                newSchema: "Menu");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "Meals",
                newSchema: "Menu");

            migrationBuilder.RenameTable(
                name: "MealOptionGroups",
                newName: "MealOptionGroups",
                newSchema: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_OptionGroupItems_OptionGroupId_Name",
                schema: "Menu",
                table: "OptionGroupItems",
                columns: new[] { "OptionGroupId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealOptionGroups_MealId_Name",
                schema: "Menu",
                table: "MealOptionGroups",
                columns: new[] { "MealId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OptionGroupItems_OptionGroupId_Name",
                schema: "Menu",
                table: "OptionGroupItems");

            migrationBuilder.DropIndex(
                name: "IX_MealOptionGroups_MealId_Name",
                schema: "Menu",
                table: "MealOptionGroups");

            migrationBuilder.RenameTable(
                name: "OptionGroupItems",
                schema: "Menu",
                newName: "OptionGroupItems");

            migrationBuilder.RenameTable(
                name: "Meals",
                schema: "Menu",
                newName: "Meals");

            migrationBuilder.RenameTable(
                name: "MealOptionGroups",
                schema: "Menu",
                newName: "MealOptionGroups");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "OptionGroupItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "MealOptionGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OptionGroupItems_OptionGroupId",
                table: "OptionGroupItems",
                column: "OptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MealOptionGroups_MealId",
                table: "MealOptionGroups",
                column: "MealId");
        }
    }
}
