using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class GoatTypeSeed
{
    public GoatTypeSeed(ref ModelBuilder builder)
    {
        var list = new List<GoatTypeEntity>();

        GoatTypeEntity.List().ToList().ForEach(i =>
        {
            list.Add(new GoatTypeEntity(i.Id, i.Name));
        });

        builder.Entity<GoatTypeEntity>().HasData(list.ToArray());
    }
}