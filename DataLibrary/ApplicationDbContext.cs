using DataLibrary.Models;
using DataLibrary.Models.Enums;
using DataLibrary.SeedMethods;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite();

    public DbSet<CalculationEntity> Calculations { get; set; }
    public DbSet<CalculationHasFeedEntity> CalculationHasFeeds { get; set; }
    public DbSet<CalculationHasResultEntity> CalculationHasResults { get; set; }
    public DbSet<CountryTranslationEntity> CountryTranslations { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<FeedEntity> Feeds { get; set; }
    public DbSet<FeedTranslationEntity> FeedTranslations { get; set; }
    public DbSet<LabelEntity> Labels { get; set; }
    public DbSet<LabelTranslationEntity> LabelTranslations { get; set; }
    public DbSet<LanguageTranslationEntity> LanguageTranslations { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<RefCountryEntity> RefCountries { get; set; }
    public DbSet<RefLanguageEntity> RefLanguages { get; set; }
    public DbSet<RefSpeciesEntity> RefSpeciesList { get; set; }
    public DbSet<SpeciesTranslationEntity> SpeciesTranslations { get; set; }
    public DbSet<SpeciesEntity> SpeciesList { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Automatically apply all configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Run seed methods
        RunAllSeedMethods(ref modelBuilder);
    }

    public void RunAllSeedMethods(ref ModelBuilder modelBuilder)
    {
        new LanguageSeed(ref modelBuilder);
        new CountrySeed(ref modelBuilder);
        new SpeciesSeed(ref modelBuilder);
        new SheepTypeSeed(ref modelBuilder);
        new GoatTypeSeed(ref modelBuilder);
        new GrazingSeed(ref modelBuilder);
        new DietQualityEstimateSeed(ref modelBuilder);
        new NrSucklingKidsLambsSeed(ref modelBuilder);
        new BodyWeightSeed(ref modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}