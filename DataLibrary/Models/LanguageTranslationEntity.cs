using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class LanguageTranslationEntity : EntityBase
{
    public LanguageTranslationEntity()
    {
    }

    public LanguageTranslationEntity(int languageId, string languageCode, string translatedDescription)
    {
        LanguageId = languageId;
        LanguageCode = languageCode;
        TranslatedDescription = translatedDescription;
    }

    public int LanguageId { get; set; } // Reference to Ref_Language.Id

    public string LanguageCode { get; set; } // NOT NULL

    public string TranslatedDescription { get; set; } // NOT NULL

    public LanguageEntity LanguageEntity { get; set; }
}