using DataLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class SpeciesSeed
{
    public SpeciesSeed(ref ModelBuilder builder)
    {
        var list = new List<SpeciesEntity>();

        SpeciesEntity.List().ToList().ForEach(i =>
        {
            list.Add(new SpeciesEntity(i.Id, i.Name));
        });

        builder.Entity<SpeciesEntity>().HasData(list.ToArray());
    }
}