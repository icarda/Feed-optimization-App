using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class RefSpeciesConfiguration : IEntityTypeConfiguration<RefSpeciesEntity>
{
    public void Configure(EntityTypeBuilder<RefSpeciesEntity> conf)
    {
        conf.ToTable("RefSpecies");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.Name).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}