using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;

public class RefSpeciesConfiguration : IEntityTypeConfiguration<RefSpeciesEntity>
{
    public void Configure(EntityTypeBuilder<RefSpeciesEntity> conf)
    {
        conf.ToTable("RefSpecies", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.Name).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}