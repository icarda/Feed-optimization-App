using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class RefCountryConfiguration : IEntityTypeConfiguration<RefCountryEntity>
{
    public void Configure(EntityTypeBuilder<RefCountryEntity> conf)
    {
        conf.ToTable("RefCountries");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.CountryId).IsRequired();
        conf.Property(c => c.DateFormat).IsRequired();
        conf.Property(c => c.CurrencyValue).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}