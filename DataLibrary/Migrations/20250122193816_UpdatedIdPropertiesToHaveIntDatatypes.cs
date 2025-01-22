using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedIdPropertiesToHaveIntDatatypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefSpeciesList");

            migrationBuilder.DropColumn(
                name: "RefCountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefLanguageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefSpeciesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "RefCountries");

            migrationBuilder.DropColumn(
                name: "BodyWeight",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "DietQualityEstimate",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "Grazing",
                table: "Calculations");

            migrationBuilder.EnsureSchema(
                name: "lut");

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

            migrationBuilder.RenameColumn(
                name: "KidsLambs",
                schema: "dbo",
                table: "Calculations",
                newName: "KidsLambsId");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "LanguageTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceVersionString",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceType",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DevicePlatform",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceModel",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceManufacturer",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceIdiom",
                schema: "dbo",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                schema: "dbo",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                schema: "dbo",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeciesId",
                schema: "dbo",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SpeciesId",
                schema: "dbo",
                table: "SpeciesTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "RefLanguages",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "RefCountries",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                schema: "dbo",
                table: "RefCountries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TranslationId",
                schema: "dbo",
                table: "LabelTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "FeedId",
                schema: "dbo",
                table: "FeedTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                schema: "dbo",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                schema: "dbo",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                schema: "dbo",
                table: "CountryTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "SpeciesId",
                schema: "dbo",
                table: "Calculations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "ADG",
                schema: "dbo",
                table: "Calculations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "BodyWeightId",
                schema: "dbo",
                table: "Calculations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DietQualityEstimateId",
                schema: "dbo",
                table: "Calculations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GrazingId",
                schema: "dbo",
                table: "Calculations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CalculationId",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "CalculationHasResults",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "CalculationId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageTranslations",
                table: "LanguageTranslations",
                column: "LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeciesTranslations",
                schema: "dbo",
                table: "SpeciesTranslations",
                columns: new[] { "SpeciesId", "LanguageCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelTranslations",
                schema: "dbo",
                table: "LabelTranslations",
                column: "TranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedTranslations",
                schema: "dbo",
                table: "FeedTranslations",
                columns: new[] { "FeedId", "LanguageCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryTranslations",
                schema: "dbo",
                table: "CountryTranslations",
                columns: new[] { "CountryId", "LanguageCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds",
                columns: new[] { "CalculationId", "FeedId" });

            migrationBuilder.CreateTable(
                name: "BodyWeightConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyWeightConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietQualityEstimateConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietQualityEstimateConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoatTypeConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoatTypeConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrazingConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrazingConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NrSucklingKidsLambsConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NrSucklingKidsLambsConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefSpecies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSpecies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SheepTypeConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheepTypeConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeciesConditions",
                schema: "lut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesConditions", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "BodyWeightConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "5" },
                    { 2, "10" },
                    { 3, "15" },
                    { 4, "20" },
                    { 5, "25" },
                    { 6, "30" },
                    { 7, "35" },
                    { 8, "40" },
                    { 9, "45" },
                    { 10, "50" },
                    { 11, "55" },
                    { 12, "60" },
                    { 13, "65" },
                    { 14, "70" },
                    { 15, "75" },
                    { 16, "80" },
                    { 17, "85" },
                    { 18, "90" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "CountryConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ethiopia" },
                    { 2, "Tunisia" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "DietQualityEstimateConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Low (< 6.5 MJ/kg DM)" },
                    { 2, "Medium (~7.5 MJ/kg DM)" },
                    { 3, "High (>8.5 MJ/kg DM)" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "GoatTypeConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Does" },
                    { 2, "Kids" },
                    { 3, "Bucks" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "GrazingConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "None" },
                    { 2, "Grazing close-by" },
                    { 3, "Open range" },
                    { 4, "Rough mountain terrain" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "LanguageConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "English / Anglais" },
                    { 2, "French / Français" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "NrSucklingKidsLambsConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "0" },
                    { 2, "1" },
                    { 3, "2" },
                    { 4, "3" },
                    { 5, "4" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "SheepTypeConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ewes" },
                    { 2, "Weaned lambs" },
                    { 3, "Rams" }
                });

            migrationBuilder.InsertData(
                schema: "lut",
                table: "SpeciesConditions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SHEEP" },
                    { 2, "GOAT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                schema: "dbo",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                schema: "dbo",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                schema: "dbo",
                table: "Users",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpeciesId",
                schema: "dbo",
                table: "Users",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesTranslations_SpeciesId_LanguageCode",
                schema: "dbo",
                table: "SpeciesTranslations",
                columns: new[] { "SpeciesId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_RefLanguages_Id",
                schema: "dbo",
                table: "RefLanguages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RefCountries_Id",
                schema: "dbo",
                table: "RefCountries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTranslations_LabelId",
                schema: "dbo",
                table: "LabelTranslations",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTranslations_TranslationId",
                schema: "dbo",
                table: "LabelTranslations",
                column: "TranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Id",
                schema: "dbo",
                table: "Labels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FeedTranslations_FeedId_LanguageCode",
                schema: "dbo",
                table: "FeedTranslations",
                columns: new[] { "FeedId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_CountryId",
                schema: "dbo",
                table: "Feeds",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Id",
                schema: "dbo",
                table: "Feeds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_LanguageId",
                schema: "dbo",
                table: "Feeds",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTranslations_CountryId_LanguageCode",
                schema: "dbo",
                table: "CountryTranslations",
                columns: new[] { "CountryId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_BodyWeightId",
                schema: "dbo",
                table: "Calculations",
                column: "BodyWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_DietQualityEstimateId",
                schema: "dbo",
                table: "Calculations",
                column: "DietQualityEstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_GrazingId",
                schema: "dbo",
                table: "Calculations",
                column: "GrazingId");

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_Id",
                schema: "dbo",
                table: "Calculations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_KidsLambsId",
                schema: "dbo",
                table: "Calculations",
                column: "KidsLambsId");

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_SpeciesId",
                schema: "dbo",
                table: "Calculations",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasResults_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults",
                column: "CalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasResults_Id",
                schema: "dbo",
                table: "CalculationHasResults",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasFeeds_CalculationId_FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                columns: new[] { "CalculationId", "FeedId" });

            migrationBuilder.CreateIndex(
                name: "IX_CalculationHasFeeds_FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_RefSpecies_Id",
                schema: "dbo",
                table: "RefSpecies",
                column: "Id");

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
                name: "FK_CalculationHasFeeds_Feeds_FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds",
                column: "FeedId",
                principalSchema: "dbo",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalculationHasResults_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults",
                column: "CalculationId",
                principalSchema: "dbo",
                principalTable: "Calculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calculations_BodyWeightConditions_BodyWeightId",
                schema: "dbo",
                table: "Calculations",
                column: "BodyWeightId",
                principalSchema: "lut",
                principalTable: "BodyWeightConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calculations_DietQualityEstimateConditions_DietQualityEstimateId",
                schema: "dbo",
                table: "Calculations",
                column: "DietQualityEstimateId",
                principalSchema: "lut",
                principalTable: "DietQualityEstimateConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calculations_GrazingConditions_GrazingId",
                schema: "dbo",
                table: "Calculations",
                column: "GrazingId",
                principalSchema: "lut",
                principalTable: "GrazingConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calculations_NrSucklingKidsLambsConditions_KidsLambsId",
                schema: "dbo",
                table: "Calculations",
                column: "KidsLambsId",
                principalSchema: "lut",
                principalTable: "NrSucklingKidsLambsConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calculations_SpeciesConditions_SpeciesId",
                schema: "dbo",
                table: "Calculations",
                column: "SpeciesId",
                principalSchema: "lut",
                principalTable: "SpeciesConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryTranslations_CountryConditions_CountryId",
                schema: "dbo",
                table: "CountryTranslations",
                column: "CountryId",
                principalSchema: "lut",
                principalTable: "CountryConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_CountryConditions_CountryId",
                schema: "dbo",
                table: "Feeds",
                column: "CountryId",
                principalSchema: "lut",
                principalTable: "CountryConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_LanguageConditions_LanguageId",
                schema: "dbo",
                table: "Feeds",
                column: "LanguageId",
                principalSchema: "lut",
                principalTable: "LanguageConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedTranslations_Feeds_FeedId",
                schema: "dbo",
                table: "FeedTranslations",
                column: "FeedId",
                principalSchema: "dbo",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelTranslations_Labels_LabelId",
                schema: "dbo",
                table: "LabelTranslations",
                column: "LabelId",
                principalSchema: "dbo",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTranslations_LanguageConditions_LanguageId",
                table: "LanguageTranslations",
                column: "LanguageId",
                principalSchema: "lut",
                principalTable: "LanguageConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpeciesTranslations_SpeciesConditions_SpeciesId",
                schema: "dbo",
                table: "SpeciesTranslations",
                column: "SpeciesId",
                principalSchema: "lut",
                principalTable: "SpeciesConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CountryConditions_CountryId",
                schema: "dbo",
                table: "Users",
                column: "CountryId",
                principalSchema: "lut",
                principalTable: "CountryConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LanguageConditions_LanguageId",
                schema: "dbo",
                table: "Users",
                column: "LanguageId",
                principalSchema: "lut",
                principalTable: "LanguageConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SpeciesConditions_SpeciesId",
                schema: "dbo",
                table: "Users",
                column: "SpeciesId",
                principalSchema: "lut",
                principalTable: "SpeciesConditions",
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
                name: "FK_CalculationHasFeeds_Feeds_FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_CalculationHasResults_Calculations_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Calculations_BodyWeightConditions_BodyWeightId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Calculations_DietQualityEstimateConditions_DietQualityEstimateId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Calculations_GrazingConditions_GrazingId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Calculations_NrSucklingKidsLambsConditions_KidsLambsId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropForeignKey(
                name: "FK_Calculations_SpeciesConditions_SpeciesId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryTranslations_CountryConditions_CountryId",
                schema: "dbo",
                table: "CountryTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_CountryConditions_CountryId",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_LanguageConditions_LanguageId",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedTranslations_Feeds_FeedId",
                schema: "dbo",
                table: "FeedTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelTranslations_Labels_LabelId",
                schema: "dbo",
                table: "LabelTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTranslations_LanguageConditions_LanguageId",
                table: "LanguageTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeciesTranslations_SpeciesConditions_SpeciesId",
                schema: "dbo",
                table: "SpeciesTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CountryConditions_CountryId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LanguageConditions_LanguageId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SpeciesConditions_SpeciesId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BodyWeightConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "CountryConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "DietQualityEstimateConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "GoatTypeConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "GrazingConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "LanguageConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "NrSucklingKidsLambsConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "RefSpecies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SheepTypeConditions",
                schema: "lut");

            migrationBuilder.DropTable(
                name: "SpeciesConditions",
                schema: "lut");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageTranslations",
                table: "LanguageTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Id",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LanguageId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SpeciesId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeciesTranslations",
                schema: "dbo",
                table: "SpeciesTranslations");

            migrationBuilder.DropIndex(
                name: "IX_SpeciesTranslations_SpeciesId_LanguageCode",
                schema: "dbo",
                table: "SpeciesTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RefLanguages_Id",
                schema: "dbo",
                table: "RefLanguages");

            migrationBuilder.DropIndex(
                name: "IX_RefCountries_Id",
                schema: "dbo",
                table: "RefCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelTranslations",
                schema: "dbo",
                table: "LabelTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LabelTranslations_LabelId",
                schema: "dbo",
                table: "LabelTranslations");

            migrationBuilder.DropIndex(
                name: "IX_LabelTranslations_TranslationId",
                schema: "dbo",
                table: "LabelTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Labels_Id",
                schema: "dbo",
                table: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedTranslations",
                schema: "dbo",
                table: "FeedTranslations");

            migrationBuilder.DropIndex(
                name: "IX_FeedTranslations_FeedId_LanguageCode",
                schema: "dbo",
                table: "FeedTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_CountryId",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_Id",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_LanguageId",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryTranslations",
                schema: "dbo",
                table: "CountryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_CountryTranslations_CountryId_LanguageCode",
                schema: "dbo",
                table: "CountryTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_BodyWeightId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_DietQualityEstimateId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_GrazingId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_Id",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_KidsLambsId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_SpeciesId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasResults_CalculationId",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasResults_Id",
                schema: "dbo",
                table: "CalculationHasResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculationHasFeeds",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_CalculationId_FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropIndex(
                name: "IX_CalculationHasFeeds_FeedId",
                schema: "dbo",
                table: "CalculationHasFeeds");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "dbo",
                table: "RefCountries");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                schema: "dbo",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "BodyWeightId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "DietQualityEstimateId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "GrazingId",
                schema: "dbo",
                table: "Calculations");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SpeciesTranslations",
                schema: "dbo",
                newName: "SpeciesTranslations");

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

            migrationBuilder.RenameColumn(
                name: "KidsLambsId",
                table: "Calculations",
                newName: "KidsLambs");

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageId",
                table: "LanguageTranslations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceVersionString",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceType",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DevicePlatform",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceModel",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceManufacturer",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceIdiom",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "RefCountryId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RefLanguageId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RefSpeciesId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SpeciesId",
                table: "SpeciesTranslations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RefLanguages",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RefCountries",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "RefCountries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TranslationId",
                table: "LabelTranslations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedId",
                table: "FeedTranslations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Feeds",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "CountryTranslations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpeciesId",
                table: "Calculations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "ADG",
                table: "Calculations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyWeight",
                table: "Calculations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DietQualityEstimate",
                table: "Calculations",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grazing",
                table: "Calculations",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "CalculationId",
                table: "CalculationHasResults",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CalculationHasResults",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedId",
                table: "CalculationHasFeeds",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "CalculationId",
                table: "CalculationHasFeeds",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "RefSpeciesList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSpeciesList", x => x.Id);
                });
        }
    }
}
