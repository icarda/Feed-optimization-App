using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CountryTranslationEntity
{
    public CountryTranslationEntity()
    {
    }

    public CountryTranslationEntity(int countryId, string languageCode, string translatedDescription)
    {
        _countryId = countryId;
        _languageCode = languageCode;
        _translatedDescription = translatedDescription;
    }

    private int _countryId;
    public int CountryId => _countryId; // Reference to Ref_Country.Id

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _translatedDescription;
    public string TranslatedDescription => _translatedDescription; // NOT NULL

    public CountryEntity Country { get; set; }
}