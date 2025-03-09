using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class SheepTypeSeed
{
    public SheepTypeSeed(ref ModelBuilder builder)
    {
        var list = new List<SheepTypeEntity>();

        SheepTypeEntity.List().ToList().ForEach(i =>
        {
            list.Add(new SheepTypeEntity(i.Id, i.Name));
        });

        builder.Entity<SheepTypeEntity>().HasData(list.ToArray());
    }
}