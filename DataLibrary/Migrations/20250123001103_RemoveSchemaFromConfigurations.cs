using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSchemaFromConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SpeciesTranslations",
                schema: "dbo",
                newName: "SpeciesTranslations");

            migrationBuilder.RenameTable(
                name: "RefSpecies",
                schema: "dbo",
                newName: "RefSpecies");

            migrationBuilder.RenameTable(
                name: "RefLanguages",
                schema: "dbo",
                newName: "RefLanguages");

            migrationBuilder.RenameTable(
                name: "RefCountries",
                schema: "dbo",
                newName: "RefCountries");

            migrationBuilder.RenameTable(
                name: "LabelTranslations",
                schema: "dbo",
                newName: "LabelTranslations");

            migrationBuilder.RenameTable(
                name: "Labels",
                schema: "dbo",
                newName: "Labels");

            migrationBuilder.RenameTable(
                name: "FeedTranslations",
                schema: "dbo",
                newName: "FeedTranslations");

            migrationBuilder.RenameTable(
                name: "Feeds",
                schema: "dbo",
                newName: "Feeds");

            migrationBuilder.RenameTable(
                name: "CountryTranslations",
                schema: "dbo",
                newName: "CountryTranslations");

            migrationBuilder.RenameTable(
                name: "Calculations",
                schema: "dbo",
                newName: "Calculations");

            migrationBuilder.RenameTable(
                name: "CalculationHasResults",
                schema: "dbo",
                newName: "CalculationHasResults");

            migrationBuilder.RenameTable(
                name: "CalculationHasFeeds",
                schema: "dbo",
                newName: "CalculationHasFeeds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SpeciesTranslations",
                newName: "SpeciesTranslations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RefSpecies",
                newName: "RefSpecies",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RefLanguages",
                newName: "RefLanguages",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RefCountries",
                newName: "RefCountries",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "LabelTranslations",
                newName: "LabelTranslations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Labels",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "FeedTranslations",
                newName: "FeedTranslations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Feeds",
                newName: "Feeds",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CountryTranslations",
                newName: "CountryTranslations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Calculations",
                newName: "Calculations",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CalculationHasResults",
                newName: "CalculationHasResults",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CalculationHasFeeds",
                newName: "CalculationHasFeeds",
                newSchema: "dbo");
        }
    }
}
