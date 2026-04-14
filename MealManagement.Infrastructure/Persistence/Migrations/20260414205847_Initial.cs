using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Menu");

            migrationBuilder.CreateTable(
                name: "Meals",
                schema: "Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    HasOptions = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealOptions",
                schema: "Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOptions_Meals_MealId",
                        column: x => x.MealId,
                        principalSchema: "Menu",
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealOptionsItems",
                schema: "Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOptionsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOptionsItems_MealOptions_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "Menu",
                        principalTable: "MealOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealOptions_MealId_Name",
                schema: "Menu",
                table: "MealOptions",
                columns: new[] { "MealId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealOptionsItems_OptionId_Name",
                schema: "Menu",
                table: "MealOptionsItems",
                columns: new[] { "OptionId", "Name" },
                unique: true);

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
            migrationBuilder.DropTable(
                name: "MealOptionsItems",
                schema: "Menu");

            migrationBuilder.DropTable(
                name: "MealOptions",
                schema: "Menu");

            migrationBuilder.DropTable(
                name: "Meals",
                schema: "Menu");
        }
    }
}
