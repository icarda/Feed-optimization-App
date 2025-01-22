using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class FeedTranslationEntity
{
    public FeedTranslationEntity()
    {
    }

    public FeedTranslationEntity(FeedEntity feed, string languageCode, string translatedDescription)
    {
        _feedId = feed.Id;
        _languageCode = languageCode;
        _translatedDescription = translatedDescription;
    }

    private int _feedId;
    public int FeedId => _feedId; // Reference to Feed.Id

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _translatedDescription;
    public string TranslatedDescription => _translatedDescription; // NOT NULL

    public FeedEntity Feed { get; set; }
}