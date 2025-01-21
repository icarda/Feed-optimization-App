using DataLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class LanguageSeed
{
    public LanguageSeed(ref ModelBuilder builder)
    {
        var list = new List<LanguageEntity>();

        LanguageEntity.List().ToList().ForEach(i =>
        {
            list.Add(new LanguageEntity(i.Id, i.Name));
        });

        builder.Entity<LanguageEntity>().HasData(list.ToArray());
    }
}