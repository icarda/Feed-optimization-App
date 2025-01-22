using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class LabelTranslationEntity
{
    public LabelTranslationEntity()
    {
    }

    public LabelTranslationEntity(int translationId, int labelId, string languageCode, string translatedText)
    {
        _translationId = translationId;
        _labelId = labelId;
        _languageCode = languageCode;
        _translatedText = translatedText;
    }

    private int _translationId;
    public int TranslationId => _translationId; // Primary key

    private int _labelId;
    public int LabelId => _labelId; // Reference to Labels.Id

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _translatedText;
    public string TranslatedText => _translatedText; // NOT NULL

    public LabelEntity LabelEntity { get; set; }
}