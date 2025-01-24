using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationHasFeedConfiguration : IEntityTypeConfiguration<CalculationHasFeedEntity>
{
    public void Configure(EntityTypeBuilder<CalculationHasFeedEntity> conf)
    {
        conf.ToTable("CalculationHasFeeds");
        conf.HasKey(c => new { c.CalculationId, c.FeedId });
        conf.Property(c => c.DM).IsRequired();
        conf.Property(c => c.CPDM).IsRequired();
        conf.Property(c => c.MEMJKGDM).IsRequired();
        conf.Property(c => c.Price).IsRequired();
        conf.Property(c => c.Intake).IsRequired();
        conf.Property(c => c.MinLimit).IsRequired();
        conf.Property(c => c.MaxLimit).IsRequired();

        conf.HasOne(c => c.Calculation)
            .WithMany()
            .HasForeignKey(c => c.CalculationId);

        conf.HasOne(c => c.Feed)
            .WithMany()
            .HasForeignKey(c => c.FeedId);

        conf.HasIndex(c => new { c.CalculationId, c.FeedId });
    }
}