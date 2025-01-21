using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;

public class SpeciesTranslationConfiguration : IEntityTypeConfiguration<SpeciesTranslationEntity>
{
    public void Configure(EntityTypeBuilder<SpeciesTranslationEntity> conf)
    {
        conf.ToTable("SpeciesTranslations", "dbo");
        conf.HasKey(c => new { c.SpeciesId, c.LanguageCode });
        conf.Property(c => c.Name).IsRequired();
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedDescription).IsRequired();

        conf.HasOne(c => c.SpeciesEntity)
            .WithMany()
            .HasForeignKey(c => c.SpeciesId);

        conf.HasIndex(c => new { c.SpeciesId, c.LanguageCode });
    }
}