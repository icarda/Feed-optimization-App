using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.SeedMethods;

public class DietQualityEstimateSeed
{
    public DietQualityEstimateSeed(ref ModelBuilder builder)
    {
        var list = new List<DietQualityEstimateEntity>();

        DietQualityEstimateEntity.List().ToList().ForEach(i =>
        {
            list.Add(new DietQualityEstimateEntity(i.Id, i.Name));
        });

        builder.Entity<DietQualityEstimateEntity>().HasData(list.ToArray());
    }
}