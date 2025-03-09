using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations.Enums
{
    public class DietQualityEstimateConfiguration : IEntityTypeConfiguration<DietQualityEstimateEntity>
    {
        public void Configure(EntityTypeBuilder<DietQualityEstimateEntity> conf)
        {
            conf.ToTable("DietQualityEstimateConditions", "lut");

            conf.HasKey(o => o.Id);

            conf.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            conf.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}