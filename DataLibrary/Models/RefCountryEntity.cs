namespace DataLibrary.Models;

public class RefCountryEntity
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

    public int Id { get; private set; } // Primary key

    public int CountryId { get; private set; } // NOT NULL

    public string DateFormat { get; private set; } // NOT NULL

    public string CurrencyValue { get; private set; } // NOT NULL
}