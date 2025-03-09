using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDuplicateIdPropertiesFromModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasFeeds_CalculationHasResults_CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasResults_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropColumn(
                name: "CalculationHasFeedIds",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropColumn(
                name: "CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds");

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

            migrationBuilder.AddColumn<int>(
                name: "CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasFeeds_CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "CalculationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationHasFeeds_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "CalculationId",
                principalSchema: "dbo",
                principalTable: "Calculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasFeeds_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasResults_CalculationHasFeeds_CalculationHasFeedId",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropColumn(
                name: "CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds");

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

            migrationBuilder.AddColumn<string>(
                name: "CalculationHasFeedIds",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasFeeds_CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "CalculationHasResultEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationHasFeeds_CalculationHasResults_CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "CalculationHasResultEntityId",
                principalSchema: "dbo",
                principalTable: "CalculationHasResults",
                principalColumn: "Id");

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
    }
}
