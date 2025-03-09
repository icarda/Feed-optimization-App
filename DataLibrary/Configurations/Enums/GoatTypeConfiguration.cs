using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations.Enums
{
    public class GoatTypeConfiguration : IEntityTypeConfiguration<GoatTypeEntity>
    {
        public void Configure(EntityTypeBuilder<GoatTypeEntity> conf)
        {
            conf.ToTable("GoatTypeConditions", "lut");

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