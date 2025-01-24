using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class RefSpeciesConfiguration : IEntityTypeConfiguration<RefSpeciesEntity>
{
    public void Configure(EntityTypeBuilder<RefSpeciesEntity> conf)
    {
        conf.ToTable("RefSpecies");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.Name).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}