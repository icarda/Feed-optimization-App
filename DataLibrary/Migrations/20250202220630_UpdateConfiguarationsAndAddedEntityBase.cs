using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfiguarationsAndAddedEntityBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasFeeds_Calculations_CalculationId",
                table: "CalculationHasFeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculationHasFeeds",
                table: "CalculationHasFeeds");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_CalculationId_FeedId",
                table: "CalculationHasFeeds");

            migrationBuilder.DropColumn(
                name: "CalculationId",
                table: "CalculationHasFeeds");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Feeds",
                newName: "Feeds",
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

            migrationBuilder.AddColumn<int>(
                name: "CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesConditions_Id",
                schema: "lut",
                table: "SpeciesConditions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesConditions_Name",
                schema: "lut",
                table: "SpeciesConditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageConditions_Id",
                schema: "lut",
                table: "LanguageConditions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageConditions_Name",
                schema: "lut",
                table: "LanguageConditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryConditions_Id",
                schema: "lut",
                table: "CountryConditions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryConditions_Name",
                schema: "lut",
                table: "CountryConditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Name",
                schema: "dbo",
                table: "Feeds",
                column: "Name",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasFeeds_CalculationHasResults_CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropIndex(
                name: "IX_SpeciesConditions_Id",
                schema: "lut",
                table: "SpeciesConditions");

            migrationBuilder.DropIndex(
                name: "IX_SpeciesConditions_Name",
                schema: "lut",
                table: "SpeciesConditions");

            migrationBuilder.DropIndex(
                name: "IX_LanguageConditions_Id",
                schema: "lut",
                table: "LanguageConditions");

            migrationBuilder.DropIndex(
                name: "IX_LanguageConditions_Name",
                schema: "lut",
                table: "LanguageConditions");

            migrationBuilder.DropIndex(
                name: "IX_CountryConditions_Id",
                schema: "lut",
                table: "CountryConditions");

            migrationBuilder.DropIndex(
                name: "IX_CountryConditions_Name",
                schema: "lut",
                table: "CountryConditions");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_Name",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropColumn(
                name: "CalculationHasResultEntityId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.RenameTable(
                name: "Feeds",
                schema: "dbo",
                newName: "Feeds");

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

            migrationBuilder.AddColumn<int>(
                name: "CalculationId",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculationHasFeeds",
                table: "CalculationHasFeeds",
                columns: new[] { "CalculationId", "FeedId" });

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasFeeds_CalculationId_FeedId",
                table: "CalculationHasFeeds",
                columns: new[] { "CalculationId", "FeedId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationHasFeeds_Calculations_CalculationId",
                table: "CalculationHasFeeds",
                column: "CalculationId",
                principalTable: "Calculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
