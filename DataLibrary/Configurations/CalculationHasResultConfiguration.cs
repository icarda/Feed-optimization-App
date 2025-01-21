using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;

public class CalculationHasResultConfiguration : IEntityTypeConfiguration<CalculationHasResultEntity>
{
    public void Configure(EntityTypeBuilder<CalculationHasResultEntity> conf)
    {
        conf.ToTable("CalculationHasResults", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.GFresh).IsRequired();
        conf.Property(c => c.PercentFresh).IsRequired();
        conf.Property(c => c.PercentDryMatter).IsRequired();
        conf.Property(c => c.TotalRation).IsRequired();

        conf.HasOne(c => c.Calculation)
            .WithMany()
            .HasForeignKey(c => c.CalculationId);

        conf.HasIndex(c => c.Id);
    }
}