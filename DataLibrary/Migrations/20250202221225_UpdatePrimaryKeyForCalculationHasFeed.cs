using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrimaryKeyForCalculationHasFeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasFeeds_Id",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_Id",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "FeedId");
        }
    }
}
