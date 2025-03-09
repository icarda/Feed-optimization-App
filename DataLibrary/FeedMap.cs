using CsvHelper.Configuration;
using DataLibrary.Models;

public class FeedMap : ClassMap<FeedEntity>
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
        Map(m => m.CountryId).Name("CountryId");
        Map(m => m.LanguageId).Name("LanguageId");
    }
}