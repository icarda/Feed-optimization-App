using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class NrSucklingKidsLambsSeed
{
    public NrSucklingKidsLambsSeed(ref ModelBuilder builder)
    {
        var list = new List<KidsLambsEntity>();

        KidsLambsEntity.List().ToList().ForEach(i =>
        {
            list.Add(new KidsLambsEntity(i.Id, i.Name));
        });

        builder.Entity<KidsLambsEntity>().HasData(list.ToArray());
    }
}