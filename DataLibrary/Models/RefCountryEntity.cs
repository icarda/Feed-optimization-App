using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class RefCountryEntity
{
    public RefCountryEntity()
    {
    }

    public RefCountryEntity(string id, string country, string dateFormat, string currencyValue)
    {
        _id = id;
        _country = country;
        _dateFormat = dateFormat;
        _currencyValue = currencyValue;
    }

    private string _id;
    public string Id => _id; // Primary key

    private string _country;
    public string Country => _country; // NOT NULL

    private string _dateFormat;
    public string DateFormat => _dateFormat; // NOT NULL

    private string _currencyValue;
    public string CurrencyValue => _currencyValue; // NOT NULL
}