using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class LabelConfiguration : IEntityTypeConfiguration<LabelEntity>
{
    public void Configure(EntityTypeBuilder<LabelEntity> conf)
    {
        conf.ToTable("Labels");
        conf.HasKey(c => c.Id);
        conf.Property(c => c.LabelKey).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}