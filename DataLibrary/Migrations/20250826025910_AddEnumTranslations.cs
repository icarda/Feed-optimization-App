using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lut",
                table: "LanguageConditions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "English");

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "LanguageConditions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "French");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lut",
                table: "LanguageConditions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "English / Anglais");

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "LanguageConditions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "French / Français");
        }
    }
}
