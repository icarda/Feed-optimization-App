using DataLibrary.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class LanguageTranslationEntity
{
    public LanguageTranslationEntity()
    {
    }

    public LanguageTranslationEntity(LanguageEntity language, string languageCode, string translatedDescription)
    {
        _languageId = language.Id.ToString();
        _languageCode = languageCode;
        _translatedDescription = translatedDescription;
    }

    private string _languageId;
    public string LanguageId => _languageId; // Reference to Ref_Language.Id

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _translatedDescription;
    public string TranslatedDescription => _translatedDescription; // NOT NULL

    public LanguageEntity LanguageEntity { get; set; }
}