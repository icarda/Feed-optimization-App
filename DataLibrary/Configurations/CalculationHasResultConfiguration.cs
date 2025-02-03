using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationHasResultConfiguration : IEntityTypeConfiguration<CalculationHasResultEntity>
{
    public void Configure(EntityTypeBuilder<CalculationHasResultEntity> conf)
    {
        conf.ToTable("CalculationHasResults", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.GFresh).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.PercentFresh).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.PercentDryMatter).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.TotalRation).IsRequired().HasPrecision(18, 2);

        conf.HasOne(c => c.CalculationHasFeed)
            .WithMany()
            .HasForeignKey(c => c.CalculationHasFeedId);

        conf.HasIndex(c => c.Id);
    }
}