namespace DataLibrary.Models;

public class RefCountryEntity
{
    public RefCountryEntity()
    {
    }

    public RefCountryEntity(int id, int countryId, string dateFormat, string currencyValue)
    {
        Id = id;
        CountryId = countryId;
        DateFormat = dateFormat;
        CurrencyValue = currencyValue;
    }

    public int Id { get; private set; } // Primary key

    public int CountryId { get; private set; } // NOT NULL

    public string DateFormat { get; private set; } // NOT NULL

    public string CurrencyValue { get; private set; } // NOT NULL
}