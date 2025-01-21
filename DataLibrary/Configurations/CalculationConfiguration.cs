using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;
public class CalculationConfiguration : IEntityTypeConfiguration<CalculationEntity>
{
    public void Configure(EntityTypeBuilder<CalculationEntity> conf)
    {
        conf.ToTable("Calculations", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.Name).IsRequired();
        conf.Property(c => c.Description);
        conf.Property(c => c.SpeciesId).IsRequired();
        conf.Property(c => c.SheepTypeId).IsRequired();
        conf.Property(c => c.GoatTypeId).IsRequired();
        conf.Property(c => c.GrazingId).IsRequired();
        conf.Property(c => c.BodyWeightId).IsRequired();
        conf.Property(c => c.ADG).IsRequired(false);
        conf.Property(c => c.Gestation).IsRequired();
        conf.Property(c => c.MilkYield).IsRequired(false);
        conf.Property(c => c.FatContent).IsRequired(false);
        conf.Property(c => c.DietQualityEstimateId).IsRequired();
        conf.Property(c => c.KidsLambsId).IsRequired();

        conf.HasIndex(c => c.Id);

        conf.HasOne(c => c.SpeciesEntity).WithMany().HasForeignKey(c => c.SpeciesId);
        conf.HasOne(c => c.SheepTypeEntity).WithMany().HasForeignKey(c => c.SheepTypeId);
        conf.HasOne(c => c.GoatTypeEntity).WithMany().HasForeignKey(c => c.GoatTypeId);
        conf.HasOne(c => c.GrazingEntity).WithMany().HasForeignKey(c => c.GrazingId);
        conf.HasOne(c => c.BodyWeightEntity).WithMany().HasForeignKey(c => c.BodyWeightId);
        conf.HasOne(c => c.DietQualityEstimateEntity).WithMany().HasForeignKey(c => c.DietQualityEstimateId);
        conf.HasOne(c => c.KidsLambsEntity).WithMany().HasForeignKey(c => c.KidsLambsId);
    }
}