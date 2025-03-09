using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationHasFeedConfiguration : IEntityTypeConfiguration<CalculationHasFeedEntity>
{
    public void Configure(EntityTypeBuilder<CalculationHasFeedEntity> conf)
    {
        conf.ToTable("CalculationHasFeeds", "dbo");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

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

        conf.HasOne(c => c.Calculation)
            .WithMany()
            .HasForeignKey(c => c.CalculationId);

        conf.HasIndex(c => c.Id);
    }
}