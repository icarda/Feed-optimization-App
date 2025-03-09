using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLibrary.Configurations.Enums
{
    public class NrSucklingKidsLambsConfiguration : IEntityTypeConfiguration<KidsLambsEntity>
    {
        public void Configure(EntityTypeBuilder<KidsLambsEntity> conf)
        {
            conf.ToTable("NrSucklingKidsLambsConditions", "lut");

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