using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class FeedConfiguration : IEntityTypeConfiguration<FeedEntity>
{
    public void Configure(EntityTypeBuilder<FeedEntity> conf)
    {
        conf.ToTable("Feeds");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.Name).IsRequired();
        conf.Property(c => c.DryMatterPercentage).IsRequired();
        conf.Property(c => c.MEMcalKg).IsRequired();
        conf.Property(c => c.MEMJKg).IsRequired();
        conf.Property(c => c.TDNPercentage).IsRequired();
        conf.Property(c => c.CPPercentage).IsRequired();
        conf.Property(c => c.DCPPercentage).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}