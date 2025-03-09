using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations;

public class LabelConfiguration : IEntityTypeConfiguration<LabelEntity>
{
    public void Configure(EntityTypeBuilder<LabelEntity> conf)
    {
        conf.ToTable("Labels");

        // Define primary key
        conf.HasKey(c => c.Id);

        // Configure Id property to be auto-incremented
        conf.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        conf.Property(c => c.LabelKey).IsRequired();

        conf.HasIndex(c => c.Id);
    }
}