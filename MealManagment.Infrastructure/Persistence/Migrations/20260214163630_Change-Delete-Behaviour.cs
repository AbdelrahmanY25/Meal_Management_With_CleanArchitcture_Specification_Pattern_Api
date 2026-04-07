using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagment.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealOptionGroups_Meals_MealId",
                table: "MealOptionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionGroupItems_MealOptionGroups_OptionGroupId",
                table: "OptionGroupItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MealOptionGroups_Meals_MealId",
                table: "MealOptionGroups",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionGroupItems_MealOptionGroups_OptionGroupId",
                table: "OptionGroupItems",
                column: "OptionGroupId",
                principalTable: "MealOptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealOptionGroups_Meals_MealId",
                table: "MealOptionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionGroupItems_MealOptionGroups_OptionGroupId",
                table: "OptionGroupItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MealOptionGroups_Meals_MealId",
                table: "MealOptionGroups",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionGroupItems_MealOptionGroups_OptionGroupId",
                table: "OptionGroupItems",
                column: "OptionGroupId",
                principalTable: "MealOptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
