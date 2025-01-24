using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class SpeciesTranslationEntity
{
    public SpeciesTranslationEntity()
    {
    }

    public SpeciesTranslationEntity(int speciesId, string name, string languageCode, string translatedDescription)
    {
        SpeciesId = speciesId;
        Name = name;
        LanguageCode = languageCode;
        TranslatedDescription = translatedDescription;
    }

    public int SpeciesId { get; private set; } // Reference to Ref_Species.Id

    public string Name { get; private set; } // NOT NULL

    public string LanguageCode { get; private set; } // NOT NULL

    public string TranslatedDescription { get; private set; } // NOT NULL

    public SpeciesEntity SpeciesEntity { get; set; }
}