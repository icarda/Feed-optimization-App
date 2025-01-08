using CsvHelper.Configuration;
using FeedOptimizationApp.Data.Models;

public class FeedMap : ClassMap<Feed>
{
    public FeedMap()
    {
        Map(m => m.Name).Name("Feed");
        Map(m => m.DryMatterPercentage).Name("Dry Matter (%)");
        Map(m => m.MEMcalKg).Name("ME (Mcal/kg)");
        Map(m => m.MEMJKg).Name("ME(MJ/kg)");
        Map(m => m.TDNPercentage).Name("TDN (%)");
        Map(m => m.CPPercentage).Name("CP (%)");
        Map(m => m.DCPPercentage).Name("DCP (%)");
    }
}