using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class FeedConfiguration : IEntityTypeConfiguration<FeedEntity>
{
    public void Configure(EntityTypeBuilder<FeedEntity> conf)
    {
        conf.ToTable("Feeds", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.Name).IsRequired();
        conf.Property(c => c.DryMatterPercentage).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MEMcalKg).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MEMJKg).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.TDNPercentage).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.CPPercentage).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.DCPPercentage).IsRequired().HasPrecision(18, 2);

        conf.HasOne(c => c.Country)
            .WithMany()
            .HasForeignKey(c => c.CountryId);

        conf.HasOne(c => c.Language)
            .WithMany()
            .HasForeignKey(c => c.LanguageId);

        conf.HasIndex(c => c.Id);
        conf.HasIndex(c => c.Name).IsUnique();
    }
}