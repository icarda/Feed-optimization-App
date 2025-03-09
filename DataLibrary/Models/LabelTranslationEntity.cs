namespace DataLibrary.Models;

public class LabelTranslationEntity : EntityBase
{
    public LabelTranslationEntity()
    {
    }

    public LabelTranslationEntity(int translationId, int labelId, string languageCode, string translatedText)
    {
        TranslationId = translationId;
        LabelId = labelId;
        LanguageCode = languageCode;
        TranslatedText = translatedText;
    }

    public int TranslationId { get; set; } // Primary key

    public int LabelId { get; set; } // Reference to Labels.Id

    public string LanguageCode { get; set; } // NOT NULL

    public string TranslatedText { get; set; } // NOT NULL

    public LabelEntity LabelEntity { get; set; }
}