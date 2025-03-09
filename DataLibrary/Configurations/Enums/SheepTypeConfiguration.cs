using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations.Enums
{
    public class SheepTypeConfiguration : IEntityTypeConfiguration<SheepTypeEntity>
    {
        public void Configure(EntityTypeBuilder<SheepTypeEntity> conf)
        {
            conf.ToTable("SheepTypeConditions", "lut");

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