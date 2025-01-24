using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class CalculationConfiguration : IEntityTypeConfiguration<CalculationEntity>
{
    public void Configure(EntityTypeBuilder<CalculationEntity> builder)
    {
        // Define table name and schema
        builder.ToTable("Calculations");

        // Define primary key
        builder.HasKey(c => c.Id);

        // Define properties
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Description);
        builder.Property(c => c.SpeciesId).IsRequired();
        builder.Property(c => c.Type).IsRequired();
        builder.Property(c => c.GrazingId).IsRequired();
        builder.Property(c => c.BodyWeightId).IsRequired();
        builder.Property(c => c.ADG).IsRequired(false);
        builder.Property(c => c.Gestation).IsRequired();
        builder.Property(c => c.MilkYield).IsRequired(false);
        builder.Property(c => c.FatContent).IsRequired(false);
        builder.Property(c => c.DietQualityEstimateId).IsRequired();
        builder.Property(c => c.KidsLambsId).IsRequired();

        // Define indexes
        builder.HasIndex(c => c.Id);

        // Define relationships
        builder.HasOne(c => c.SpeciesEntity).WithMany().HasForeignKey(c => c.SpeciesId);
        builder.HasOne(c => c.GrazingEntity).WithMany().HasForeignKey(c => c.GrazingId);
        builder.HasOne(c => c.BodyWeightEntity).WithMany().HasForeignKey(c => c.BodyWeightId);
        builder.HasOne(c => c.DietQualityEstimateEntity).WithMany().HasForeignKey(c => c.DietQualityEstimateId);
        builder.HasOne(c => c.KidsLambsEntity).WithMany().HasForeignKey(c => c.KidsLambsId);
    }
}