using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class RefLanguageConfiguration : IEntityTypeConfiguration<RefLanguageEntity>
{
    public void Configure(EntityTypeBuilder<RefLanguageEntity> conf)
    {
        conf.ToTable("RefLanguages");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.LanguageCode).IsRequired();
        conf.Property(c => c.Name).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}