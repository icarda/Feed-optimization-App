using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CountryTranslationEntity
{
    public CountryTranslationEntity()
    {
    }

    public CountryTranslationEntity(int countryId, string languageCode, string translatedDescription)
    {
        CountryId = countryId;
        LanguageCode = languageCode;
        TranslatedDescription = translatedDescription;
    }

    public int CountryId { get; private set; } // Reference to Ref_Country.Id

    public string LanguageCode { get; private set; } // NOT NULL

    public string TranslatedDescription { get; private set; } // NOT NULL

    public CountryEntity Country { get; set; }
}