using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class FeedTranslationConfiguration : IEntityTypeConfiguration<FeedTranslationEntity>
{
    public void Configure(EntityTypeBuilder<FeedTranslationEntity> conf)
    {
        conf.ToTable("FeedTranslations");
        conf.HasKey(c => new { c.FeedId, c.LanguageCode });
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedDescription).IsRequired();

        conf.HasOne(c => c.Feed)
            .WithMany()
            .HasForeignKey(c => c.FeedId);

        conf.HasIndex(c => new { c.FeedId, c.LanguageCode });
    }
}