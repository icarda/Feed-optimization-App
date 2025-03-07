using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedEwesAndLambsToSheepTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SheepTypeConditions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Ewes and lambs");

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SheepTypeConditions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Weaned lambs");

            migrationBuilder.InsertData(
                schema: "lut",
                table: "SheepTypeConditions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Rams" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lut",
                table: "SheepTypeConditions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SheepTypeConditions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Weaned lambs");

            migrationBuilder.UpdateData(
                schema: "lut",
                table: "SheepTypeConditions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Rams");
        }
    }
}
