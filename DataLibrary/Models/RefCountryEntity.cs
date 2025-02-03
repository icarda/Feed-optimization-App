namespace DataLibrary.Models;

public class RefCountryEntity : EntityBase
{
    public RefCountryEntity()
    {
    }

    public RefCountryEntity(int countryId, string dateFormat, string currencyValue)
    {
        CountryId = countryId;
        DateFormat = dateFormat;
        CurrencyValue = currencyValue;
    }

    public int CountryId { get; set; } // NOT NULL

    public string DateFormat { get; set; } // NOT NULL

    public string CurrencyValue { get; set; } // NOT NULL
}