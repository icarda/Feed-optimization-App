using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationHasFeeds",
                columns: table => new
                {
                    CalculationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FeedId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DM = table.Column<int>(type: "INTEGER", nullable: false),
                    CPDM = table.Column<int>(type: "INTEGER", nullable: false),
                    MEMJKGDM = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Intake = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxLimit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CalculationHasResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CalculationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GFresh = table.Column<int>(type: "INTEGER", nullable: false),
                    PercentFresh = table.Column<int>(type: "INTEGER", nullable: false),
                    PercentDryMatter = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalRation = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationHasResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpeciesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Grazing = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BodyWeight = table.Column<int>(type: "INTEGER", nullable: false),
                    ADG = table.Column<int>(type: "INTEGER", nullable: false),
                    DietQualityEstimate = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Gestation = table.Column<bool>(type: "INTEGER", nullable: false),
                    MilkYield = table.Column<int>(type: "INTEGER", nullable: true),
                    FatContent = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryTranslations",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TranslatedDescription = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DryMatterPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    MEMcalKg = table.Column<decimal>(type: "TEXT", nullable: false),
                    MEMJKg = table.Column<double>(type: "REAL", nullable: false),
                    TDNPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    CPPercentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    DCPPercentage = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedTranslations",
                columns: table => new
                {
                    FeedId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TranslatedDescription = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LabelKey = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabelTranslations",
                columns: table => new
                {
                    TranslationId = table.Column<int>(type: "INTEGER", nullable: false),
                    LabelId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TranslatedText = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LanguageTranslations",
                columns: table => new
                {
                    LanguageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TranslatedDescription = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RefCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    DateFormat = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyValue = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefLanguages", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "SpeciesTranslations",
                columns: table => new
                {
                    SpeciesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TranslatedDescription = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RefCountryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RefLanguageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RefSpeciesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TermsAndConditions = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationHasFeeds");

            migrationBuilder.DropTable(
                name: "CalculationHasResults");

            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "CountryTranslations");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropTable(
                name: "FeedTranslations");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "LabelTranslations");

            migrationBuilder.DropTable(
                name: "LanguageTranslations");

            migrationBuilder.DropTable(
                name: "RefCountries");

            migrationBuilder.DropTable(
                name: "RefLanguages");

            migrationBuilder.DropTable(
                name: "RefSpeciesList");

            migrationBuilder.DropTable(
                name: "SpeciesTranslations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}