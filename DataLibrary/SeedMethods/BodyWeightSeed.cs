using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class BodyWeightSeed
{
    public BodyWeightSeed(ref ModelBuilder builder)
    {
        var list = new List<BodyWeightEntity>();

        BodyWeightEntity.List().ToList().ForEach(i =>
        {
            list.Add(new BodyWeightEntity(i.Id, i.Name));
        });

        builder.Entity<BodyWeightEntity>().HasData(list.ToArray());
    }
}