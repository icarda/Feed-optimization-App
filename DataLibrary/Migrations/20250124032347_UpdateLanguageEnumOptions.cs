using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLanguageEnumOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SpeciesConditions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Sheep");

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SpeciesConditions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Goat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SpeciesConditions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "SHEEP");

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SpeciesConditions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "GOAT");
        }
    }
}
