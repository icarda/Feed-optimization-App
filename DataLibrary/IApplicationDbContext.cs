using DataLibrary.Models;
using DataLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

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
    }
}