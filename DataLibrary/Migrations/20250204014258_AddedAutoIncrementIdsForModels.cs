using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedAutoIncrementIdsForModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeciesTranslations",
                table: "SpeciesTranslations");

            migrationBuilder.DropIndex(
                name: "IX_SpeciesTranslations_SpeciesId_LanguageCode",
                table: "SpeciesTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageTranslations",
                table: "LanguageTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelTranslations",
                table: "LabelTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LabelTranslations_TranslationId",
                table: "LabelTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedTranslations",
                table: "FeedTranslations");

            migrationBuilder.DropIndex(
                name: "IX_FeedTranslations_FeedId_LanguageCode",
                table: "FeedTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryTranslations",
                table: "CountryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CountryTranslations_CountryId_LanguageCode",
                table: "CountryTranslations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SpeciesTranslations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LanguageTranslations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "TranslationId",
                table: "LabelTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LabelTranslations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FeedTranslations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CountryTranslations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeciesTranslations",
                table: "SpeciesTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageTranslations",
                table: "LanguageTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelTranslations",
                table: "LabelTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedTranslations",
                table: "FeedTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryTranslations",
                table: "CountryTranslations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesTranslations_Id",
                table: "SpeciesTranslations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesTranslations_SpeciesId",
                table: "SpeciesTranslations",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTranslations_Id",
                table: "LanguageTranslations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTranslations_LanguageId",
                table: "LanguageTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTranslations_Id",
                table: "LabelTranslations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FeedTranslations_FeedId",
                table: "FeedTranslations",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedTranslations_Id",
                table: "FeedTranslations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTranslations_CountryId",
                table: "CountryTranslations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTranslations_Id",
                table: "CountryTranslations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeciesTranslations",
                table: "SpeciesTranslations");

            migrationBuilder.DropIndex(
                name: "IX_SpeciesTranslations_Id",
                table: "SpeciesTranslations");

            migrationBuilder.DropIndex(
                name: "IX_SpeciesTranslations_SpeciesId",
                table: "SpeciesTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageTranslations",
                table: "LanguageTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTranslations_Id",
                table: "LanguageTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTranslations_LanguageId",
                table: "LanguageTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelTranslations",
                table: "LabelTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LabelTranslations_Id",
                table: "LabelTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedTranslations",
                table: "FeedTranslations");

            migrationBuilder.DropIndex(
                name: "IX_FeedTranslations_FeedId",
                table: "FeedTranslations");

            migrationBuilder.DropIndex(
                name: "IX_FeedTranslations_Id",
                table: "FeedTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryTranslations",
                table: "CountryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CountryTranslations_CountryId",
                table: "CountryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CountryTranslations_Id",
                table: "CountryTranslations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SpeciesTranslations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LanguageTranslations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LabelTranslations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FeedTranslations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CountryTranslations");

            migrationBuilder.AlterColumn<int>(
                name: "TranslationId",
                table: "LabelTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeciesTranslations",
                table: "SpeciesTranslations",
                columns: new[] { "SpeciesId", "LanguageCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageTranslations",
                table: "LanguageTranslations",
                column: "LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelTranslations",
                table: "LabelTranslations",
                column: "TranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedTranslations",
                table: "FeedTranslations",
                columns: new[] { "FeedId", "LanguageCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryTranslations",
                table: "CountryTranslations",
                columns: new[] { "CountryId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesTranslations_SpeciesId_LanguageCode",
                table: "SpeciesTranslations",
                columns: new[] { "SpeciesId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_LabelTranslations_TranslationId",
                table: "LabelTranslations",
                column: "TranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedTranslations_FeedId_LanguageCode",
                table: "FeedTranslations",
                columns: new[] { "FeedId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_CountryTranslations_CountryId_LanguageCode",
                table: "CountryTranslations",
                columns: new[] { "CountryId", "LanguageCode" });
        }
    }
}
