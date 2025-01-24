namespace DataLibrary.Models;

public class LabelTranslationEntity
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

    public int TranslationId { get; private set; } // Primary key

    public int LabelId { get; private set; } // Reference to Labels.Id

    public string LanguageCode { get; private set; } // NOT NULL

    public string TranslatedText { get; private set; } // NOT NULL

    public LabelEntity LabelEntity { get; set; }
}