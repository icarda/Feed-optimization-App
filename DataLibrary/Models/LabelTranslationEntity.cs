using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class LabelTranslationEntity
{
    public LabelTranslationEntity()
    {
    }

    public LabelTranslationEntity(string translationId, LabelEntity label, string languageCode, string translatedText)
    {
        _translationId = translationId;
        _labelId = label.Id;
        _languageCode = languageCode;
        _translatedText = translatedText;
    }

    private string _translationId;
    public string TranslationId => _translationId; // Primary key

    private string _labelId;
    public string LabelId => _labelId; // Reference to Labels.Id

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _translatedText;
    public string TranslatedText => _translatedText; // NOT NULL

    public LabelEntity LabelEntity { get; set; }
}