using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class SpeciesTranslationEntity : EntityBase
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

    public int SpeciesId { get; set; } // Reference to Ref_Species.Id

    public string Name { get; set; } // NOT NULL

    public string LanguageCode { get; set; } // NOT NULL

    public string TranslatedDescription { get; set; } // NOT NULL

    public SpeciesEntity SpeciesEntity { get; set; }
}