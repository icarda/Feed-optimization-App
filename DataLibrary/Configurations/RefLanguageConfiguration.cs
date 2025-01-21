using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;

public class RefLanguageConfiguration : IEntityTypeConfiguration<RefLanguageEntity>
{
    public void Configure(EntityTypeBuilder<RefLanguageEntity> conf)
    {
        conf.ToTable("RefLanguages", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.Name).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}