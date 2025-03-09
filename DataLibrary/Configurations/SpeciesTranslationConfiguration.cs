using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class SpeciesTranslationConfiguration : IEntityTypeConfiguration<SpeciesTranslationEntity>
{
    public void Configure(EntityTypeBuilder<SpeciesTranslationEntity> conf)
    {
        conf.ToTable("SpeciesTranslations");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.Name).IsRequired();
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedDescription).IsRequired();

        conf.HasOne(c => c.SpeciesEntity)
            .WithMany()
            .HasForeignKey(c => c.SpeciesId);

        conf.HasIndex(c => new { c.Id });
    }
}