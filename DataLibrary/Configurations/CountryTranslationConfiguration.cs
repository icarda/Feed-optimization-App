using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CountryTranslationConfiguration : IEntityTypeConfiguration<CountryTranslationEntity>
{
    public void Configure(EntityTypeBuilder<CountryTranslationEntity> conf)
    {
        conf.ToTable("CountryTranslations");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedDescription).IsRequired();

        conf.HasOne(c => c.Country)
            .WithMany()
            .HasForeignKey(c => c.CountryId);

        conf.HasIndex(c => new { c.Id });
    }
}