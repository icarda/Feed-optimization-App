using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class FeedEntity
{
    public FeedEntity()
    {
    }

    public FeedEntity(string name, decimal dryMatterPercentage, decimal memcalKg, decimal memjKg, decimal tdnPercentage, decimal cpPercentage, decimal dcpPercentage, int countryId, int languageId)
    {
        Name = name;
        DryMatterPercentage = dryMatterPercentage;
        MEMcalKg = memcalKg;
        MEMJKg = memjKg;
        TDNPercentage = tdnPercentage;
        CPPercentage = cpPercentage;
        DCPPercentage = dcpPercentage;
        CountryId = countryId;
        LanguageId = languageId;
    }

    public void Set(string name, decimal dryMatterPercentage, decimal memcalKg, decimal memjKg, decimal tdnPercentage, decimal cpPercentage, decimal dcpPercentage, int countryId, int languageId)
    {
        Name = name;
        DryMatterPercentage = dryMatterPercentage;
        MEMcalKg = memcalKg;
        MEMJKg = memjKg;
        TDNPercentage = tdnPercentage;
        CPPercentage = cpPercentage;
        DCPPercentage = dcpPercentage;
        CountryId = countryId;
        LanguageId = languageId;
    }

    public int Id { get; set; } // Primary key

    public string Name { get; set; } // NOT NULL

    public decimal DryMatterPercentage { get; set; } // NOT NULL

    public decimal MEMcalKg { get; set; } // NOT NULL

    public decimal MEMJKg { get; set; } // NOT NULL

    public decimal TDNPercentage { get; set; } // NOT NULL

    public decimal CPPercentage { get; set; } // NOT NULL

    public decimal DCPPercentage { get; set; } // NOT NULL

    public int CountryId { get; set; }

    public int LanguageId { get; set; }

    public CountryEntity Country { get; set; }
    public LanguageEntity Language { get; set; }
}