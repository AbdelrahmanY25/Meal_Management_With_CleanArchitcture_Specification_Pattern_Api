using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTablesNameAndHasOptionsColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionGroupItems",
                schema: "Menu");

            migrationBuilder.DropTable(
                name: "MealOptionGroups",
                schema: "Menu");

            migrationBuilder.RenameColumn(
                name: "HasOptionGroup",
                schema: "Menu",
                table: "Meals",
                newName: "HasOptions");

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
                    OptionGroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOptionsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOptionsItems_MealOptions_OptionGroupId",
                        column: x => x.OptionGroupId,
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
                name: "IX_MealOptionsItems_OptionGroupId_Name",
                schema: "Menu",
                table: "MealOptionsItems",
                columns: new[] { "OptionGroupId", "Name" },
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

            migrationBuilder.RenameColumn(
                name: "HasOptions",
                schema: "Menu",
                table: "Meals",
                newName: "HasOptionGroup");

            migrationBuilder.CreateTable(
                name: "MealOptionGroups",
                schema: "Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOptionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealOptionGroups_Meals_MealId",
                        column: x => x.MealId,
                        principalSchema: "Menu",
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OptionGroupItems",
                schema: "Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionGroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionGroupItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionGroupItems_MealOptionGroups_OptionGroupId",
                        column: x => x.OptionGroupId,
                        principalSchema: "Menu",
                        principalTable: "MealOptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealOptionGroups_MealId_Name",
                schema: "Menu",
                table: "MealOptionGroups",
                columns: new[] { "MealId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionGroupItems_OptionGroupId_Name",
                schema: "Menu",
                table: "OptionGroupItems",
                columns: new[] { "OptionGroupId", "Name" },
                unique: true);
        }
    }
}
