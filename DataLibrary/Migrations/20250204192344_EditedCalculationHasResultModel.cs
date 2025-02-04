using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class EditedCalculationHasResultModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasResults_CalculationHasFeeds_CalculationHasFeedId",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.RenameColumn(
                name: "CalculationHasFeedId",
                schema: "dbo",
                table: "CalculationHasResults",
                newName: "CalculationId");

            migrationBuilder.RenameIndex(
                name: "IX_CalculationHasResults_CalculationHasFeedId",
                schema: "dbo",
                table: "CalculationHasResults",
                newName: "IX_CalculationHasResults_CalculationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationHasResults_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults",
                column: "CalculationId",
                principalSchema: "dbo",
                principalTable: "Calculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasResults_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.RenameColumn(
                name: "CalculationId",
                schema: "dbo",
                table: "CalculationHasResults",
                newName: "CalculationHasFeedId");

            migrationBuilder.RenameIndex(
                name: "IX_CalculationHasResults_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults",
                newName: "IX_CalculationHasResults_CalculationHasFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationHasResults_CalculationHasFeeds_CalculationHasFeedId",
                schema: "dbo",
                table: "CalculationHasResults",
                column: "CalculationHasFeedId",
                principalSchema: "dbo",
                principalTable: "CalculationHasFeeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
