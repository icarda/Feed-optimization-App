using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CountryTranslationConfiguration : IEntityTypeConfiguration<CountryTranslationEntity>
{
    public void Configure(EntityTypeBuilder<CountryTranslationEntity> conf)
    {
        conf.ToTable("CountryTranslations");
        conf.HasKey(c => new { c.CountryId, c.LanguageCode });
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedDescription).IsRequired();

        conf.HasOne(c => c.Country)
            .WithMany()
            .HasForeignKey(c => c.CountryId);

        conf.HasIndex(c => new { c.CountryId, c.LanguageCode });
    }
}