using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddNutrientRequirementsToCalculationHasResultEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CPi",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CPiRequirement",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMi",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMiRequirement",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MEi",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MEiRequirement",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPi",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "CPiRequirement",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "Cost",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "DMi",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "DMiRequirement",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "MEi",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "MEiRequirement",
                schema: "dbo",
                table: "CalculationHasResults");
        }
    }
}
