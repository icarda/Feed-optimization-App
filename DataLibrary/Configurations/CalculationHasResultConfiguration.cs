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

        conf.HasOne(c => c.Calculation)
            .WithMany()
            .HasForeignKey(c => c.CalculationId);

        var calculationHasFeedList = conf.Metadata.FindNavigation(nameof(CalculationHasResultEntity.CalculationHasFeedList));
        calculationHasFeedList.SetPropertyAccessMode(PropertyAccessMode.Field);

        conf.HasIndex(c => c.Id);
    }
}