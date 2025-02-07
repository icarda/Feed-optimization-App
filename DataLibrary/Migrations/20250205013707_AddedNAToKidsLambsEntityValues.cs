using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedNAToKidsLambsEntityValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "lut",
                table: "NrSucklingKidsLambsConditions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "N/A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "lut",
                table: "NrSucklingKidsLambsConditions",
                keyColumn: "Id",
                keyValue: 0);
        }
    }
}
