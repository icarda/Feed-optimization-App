using DataLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class CountrySeed
{
    public CountrySeed(ref ModelBuilder builder)
    {
        var list = new List<CountryEntity>();

        CountryEntity.List().ToList().ForEach(i =>
        {
            list.Add(new CountryEntity(i.Id, i.Name));
        });

        builder.Entity<CountryEntity>().HasData(list.ToArray());
    }
}