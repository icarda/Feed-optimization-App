using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddRequirementPropertiesToCalculationHasResultEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CrudeProteinRequirementAdditional",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CrudeProteinRequirementMaintenance",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DryMatterIntakeEstimateAdditional",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DryMatterIntakeEstimateBase",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EnergyRequirementAdditional",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EnergyRequirementMaintenance",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EnergyRequirementTotal",
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
                name: "CrudeProteinRequirementAdditional",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "CrudeProteinRequirementMaintenance",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "DryMatterIntakeEstimateAdditional",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "DryMatterIntakeEstimateBase",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "EnergyRequirementAdditional",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "EnergyRequirementMaintenance",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "EnergyRequirementTotal",
                schema: "dbo",
                table: "CalculationHasResults");
        }
    }
}
