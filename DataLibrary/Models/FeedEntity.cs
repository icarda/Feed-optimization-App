using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class FeedEntity
{
    public FeedEntity()
    {
    }

    public FeedEntity(int id, string name, decimal dryMatterPercentage, decimal memcalKg, decimal memjKg, decimal tdnPercentage, decimal cpPercentage, decimal dcpPercentage, int countryId, int languageId)
    {
        _id = id;
        _name = name;
        _dryMatterPercentage = dryMatterPercentage;
        _memcalKg = memcalKg;
        _memjKg = memjKg;
        _tdnPercentage = tdnPercentage;
        _cpPercentage = cpPercentage;
        _dcpPercentage = dcpPercentage;
        _countryId = countryId;
        _languageId = languageId;
    }

    public void Set(string name, decimal dryMatterPercentage, decimal memcalKg, decimal memjKg, decimal tdnPercentage, decimal cpPercentage, decimal dcpPercentage, int countryId, int languageId)
    {
        _name = name;
        _dryMatterPercentage = dryMatterPercentage;
        _memcalKg = memcalKg;
        _memjKg = memjKg;
        _tdnPercentage = tdnPercentage;
        _cpPercentage = cpPercentage;
        _dcpPercentage = dcpPercentage;
        _countryId = countryId;
        _languageId = languageId;
    }

    private int _id;
    public int Id => _id; // Primary key

    private string _name;
    public string Name => _name; // NOT NULL

    private decimal _dryMatterPercentage;
    public decimal DryMatterPercentage => _dryMatterPercentage; // NOT NULL

    private decimal _memcalKg;
    public decimal MEMcalKg => _memcalKg; // NOT NULL

    private decimal _memjKg;
    public decimal MEMJKg => _memjKg; // NOT NULL

    private decimal _tdnPercentage;
    public decimal TDNPercentage => _tdnPercentage; // NOT NULL

    private decimal _cpPercentage;
    public decimal CPPercentage => _cpPercentage; // NOT NULL

    private decimal _dcpPercentage;
    public decimal DCPPercentage => _dcpPercentage; // NOT NULL

    private int _countryId;
    public int CountryId => _countryId;

    private int _languageId;
    public int LanguageId => _languageId;

    public CountryEntity Country { get; set; }
    public LanguageEntity Language { get; set; }
}