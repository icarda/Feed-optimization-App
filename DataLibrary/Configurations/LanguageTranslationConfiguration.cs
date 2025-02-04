using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Models.Configurations
{
    public class LanguageTranslationConfiguration : IEntityTypeConfiguration<LanguageTranslationEntity>
    {
        public void Configure(EntityTypeBuilder<LanguageTranslationEntity> builder)
        {
            // Table name
            builder.ToTable("LanguageTranslations");

            // Define primary key
            builder.HasKey(c => c.Id);

            // Configure Id property to be auto-incremented
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(e => e.LanguageId)
                .IsRequired();

            builder.Property(e => e.LanguageCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.TranslatedDescription)
                .IsRequired()
                .HasMaxLength(255);

            // Relationships
            builder.HasOne(e => e.LanguageEntity)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.Id);
        }
    }
}