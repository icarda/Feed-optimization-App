using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class GrazingSeed
{
    public GrazingSeed(ref ModelBuilder builder)
    {
        var list = new List<GrazingEntity>();

        GrazingEntity.List().ToList().ForEach(i =>
        {
            list.Add(new GrazingEntity(i.Id, i.Name));
        });

        builder.Entity<GrazingEntity>().HasData(list.ToArray());
    }
}