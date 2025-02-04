using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class LabelTranslationConfiguration : IEntityTypeConfiguration<LabelTranslationEntity>
{
    public void Configure(EntityTypeBuilder<LabelTranslationEntity> conf)
    {
        conf.ToTable("LabelTranslations");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.TranslationId).IsRequired();
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.TranslatedText).IsRequired();

        conf.HasOne(c => c.LabelEntity)
            .WithMany()
            .HasForeignKey(c => c.LabelId);

        conf.HasIndex(c => c.Id);
    }
}