using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;

public class LabelTranslationConfiguration : IEntityTypeConfiguration<LabelTranslationEntity>
{
    public void Configure(EntityTypeBuilder<LabelTranslationEntity> conf)
    {
        conf.ToTable("LabelTranslations", "dbo");
        conf.HasKey(c => c.TranslationId);
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedText).IsRequired();

        conf.HasOne(c => c.LabelEntity)
            .WithMany()
            .HasForeignKey(c => c.LabelId);

        conf.HasIndex(c => c.TranslationId);
    }
}