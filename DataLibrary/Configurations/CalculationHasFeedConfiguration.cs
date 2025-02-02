using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationHasFeedConfiguration : IEntityTypeConfiguration<CalculationHasFeedEntity>
{
    public void Configure(EntityTypeBuilder<CalculationHasFeedEntity> conf)
    {
        conf.ToTable("CalculationHasFeeds", "dbo");
        conf.HasKey(c => c.FeedId);
        conf.Property(c => c.DM).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.CPDM).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MEMJKGDM).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.Price).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.Intake).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MinLimit).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MaxLimit).IsRequired().HasPrecision(18, 2);

        conf.HasOne(c => c.Feed)
            .WithMany()
            .HasForeignKey(c => c.FeedId);

        conf.HasIndex(c => c.FeedId);
    }
}