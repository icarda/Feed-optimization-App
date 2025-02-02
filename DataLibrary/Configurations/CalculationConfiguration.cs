using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationConfiguration : IEntityTypeConfiguration<CalculationEntity>
{
    public void Configure(EntityTypeBuilder<CalculationEntity> conf)
    {
        // Define table name and schema
        conf.ToTable("Calculations", "dbo");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Define properties
        conf.Property(c => c.Name).IsRequired();
        conf.Property(c => c.Description);
        conf.Property(c => c.SpeciesId).IsRequired();
        conf.Property(c => c.Type).IsRequired();
        conf.Property(c => c.GrazingId).IsRequired();
        conf.Property(c => c.BodyWeightId).IsRequired();
        conf.Property(c => c.ADG).IsRequired(false).HasPrecision(18, 2);
        conf.Property(c => c.Gestation).IsRequired();
        conf.Property(c => c.MilkYield).IsRequired(false).HasPrecision(18, 2);
        conf.Property(c => c.FatContent).IsRequired(false).HasPrecision(18, 2);
        conf.Property(c => c.DietQualityEstimateId).IsRequired();
        conf.Property(c => c.KidsLambsId).IsRequired();

        // Define relationships
        conf.HasOne(c => c.SpeciesEntity).WithMany().HasForeignKey(c => c.SpeciesId);
        conf.HasOne(c => c.GrazingEntity).WithMany().HasForeignKey(c => c.GrazingId);
        conf.HasOne(c => c.BodyWeightEntity).WithMany().HasForeignKey(c => c.BodyWeightId);
        conf.HasOne(c => c.DietQualityEstimateEntity).WithMany().HasForeignKey(c => c.DietQualityEstimateId);
        conf.HasOne(c => c.KidsLambsEntity).WithMany().HasForeignKey(c => c.KidsLambsId);

        // Define indexes
        conf.HasIndex(c => c.Id);
    }
}