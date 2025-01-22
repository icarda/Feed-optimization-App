using DataLibrary.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class SpeciesTranslationEntity
{
    public SpeciesTranslationEntity()
    {
    }

    public SpeciesTranslationEntity(int speciesId, string name, string languageCode, string translatedDescription)
    {
        _speciesId = speciesId;
        _name = name;
        _languageCode = languageCode;
        _translatedDescription = translatedDescription;
    }

    private int _speciesId;
    public int SpeciesId => _speciesId; // Reference to Ref_Species.Id

    private string _name;
    public string Name => _name; // NOT NULL

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _translatedDescription;
    public string TranslatedDescription => _translatedDescription; // NOT NULL

    public SpeciesEntity SpeciesEntity { get; set; }
}