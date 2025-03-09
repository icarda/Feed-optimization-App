using DataLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations.Enums
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<SpeciesEntity>
    {
        public void Configure(EntityTypeBuilder<SpeciesEntity> conf)
        {
            conf.ToTable("SpeciesConditions", "lut");

            conf.HasKey(o => o.Id);

            conf.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            conf.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();

            conf.HasIndex(o => o.Id);
            conf.HasIndex(o => o.Name).IsUnique();
        }
    }
}