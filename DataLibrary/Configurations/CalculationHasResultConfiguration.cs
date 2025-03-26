using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationHasResultConfiguration : IEntityTypeConfiguration<CalculationHasResultEntity>
{
    public void Configure(EntityTypeBuilder<CalculationHasResultEntity> conf)
    {
        conf.ToTable("CalculationHasResults", "dbo");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.GFresh).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.PercentFresh).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.PercentDryMatter).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.TotalRation).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.DMi).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.CPi).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MEi).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.Cost).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.DMiRequirement).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.CPiRequirement).IsRequired().HasPrecision(18, 2);
        conf.Property(c => c.MEiRequirement).IsRequired().HasPrecision(18, 2);

        conf.HasOne(c => c.Calculation)
            .WithMany()
            .HasForeignKey(c => c.CalculationId);

        conf.HasIndex(c => c.Id);
    }
}