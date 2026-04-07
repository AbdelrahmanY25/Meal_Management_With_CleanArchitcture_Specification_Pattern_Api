using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagment.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealOptionGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOptionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOptionGroups_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionGroupItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionGroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsPobular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionGroupItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionGroupItems_MealOptionGroups_OptionGroupId",
                        column: x => x.OptionGroupId,
                        principalTable: "MealOptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealOptionGroups_MealId",
                table: "MealOptionGroups",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionGroupItems_OptionGroupId",
                table: "OptionGroupItems",
                column: "OptionGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionGroupItems");

            migrationBuilder.DropTable(
                name: "MealOptionGroups");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
