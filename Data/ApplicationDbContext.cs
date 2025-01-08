using FeedOptimizationApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedOptimizationApp.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public DbSet<Calculation> Calculations { get; set; }
    public DbSet<CalculationHasFeed> CalculationHasFeeds { get; set; }
    public DbSet<CalculationHasResult> CalculationHasResults { get; set; }
    public DbSet<CountryTranslation> CountryTranslations { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<FeedTranslation> FeedTranslations { get; set; }
    public DbSet<Data.Models.Label> Labels { get; set; }
    public DbSet<LabelTranslation> LabelTranslations { get; set; }
    public DbSet<LanguageTranslation> LanguageTranslations { get; set; }
    public DbSet<RefCountry> RefCountries { get; set; }
    public DbSet<RefLanguage> RefLanguages { get; set; }
    public DbSet<RefSpecies> RefSpeciesList { get; set; }
    public DbSet<SpeciesTranslation> SpeciesTranslations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=FeedOptimizationApp.Database.db3");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Additional model configuration if needed
        modelBuilder.Entity<Calculation>();
        modelBuilder.Entity<CalculationHasFeed>().HasNoKey();
        modelBuilder.Entity<CalculationHasResult>();
        modelBuilder.Entity<CountryTranslation>().HasNoKey();
        modelBuilder.Entity<Feed>();
        modelBuilder.Entity<FeedTranslation>().HasNoKey();
        modelBuilder.Entity<Data.Models.Label>();
        modelBuilder.Entity<LabelTranslation>().HasNoKey();
        modelBuilder.Entity<LanguageTranslation>().HasNoKey();
        modelBuilder.Entity<RefCountry>();
        modelBuilder.Entity<RefLanguage>();
        modelBuilder.Entity<RefSpecies>();
        modelBuilder.Entity<SpeciesTranslation>().HasNoKey();
        modelBuilder.Entity<User>();
    }
}