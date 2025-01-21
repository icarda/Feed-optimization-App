using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLibrary.Models;

namespace DataLibrary.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> conf)
    {
        conf.ToTable("Users", "dbo");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.CountryId).IsRequired();
        conf.Property(c => c.LanguageId).IsRequired();
        conf.Property(c => c.SpeciesId).IsRequired();
        conf.Property(c => c.TermsAndConditions).IsRequired();
        conf.Property(c => c.CreatedAt).IsRequired();
        conf.Property(c => c.DeviceManufacturer).IsRequired(false);
        conf.Property(c => c.DeviceModel).IsRequired(false);
        conf.Property(c => c.DeviceName).IsRequired(false);
        conf.Property(c => c.DeviceVersionString).IsRequired(false);
        conf.Property(c => c.DevicePlatform).IsRequired(false);
        conf.Property(c => c.DeviceIdiom).IsRequired(false);
        conf.Property(c => c.DeviceType).IsRequired(false);

        conf.HasOne(c => c.CountryEntity)
            .WithMany()
            .HasForeignKey(c => c.CountryId);

        conf.HasOne(c => c.LanguageEntity)
            .WithMany()
            .HasForeignKey(c => c.LanguageId);

        conf.HasOne(c => c.SpeciesEntity)
            .WithMany()
            .HasForeignKey(c => c.SpeciesId);

        conf.HasIndex(c => c.Id);
    }
}