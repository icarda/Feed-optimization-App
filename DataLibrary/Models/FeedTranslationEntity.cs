namespace DataLibrary.Models;

public class FeedTranslationEntity
{
    public FeedTranslationEntity()
    {
    }

    public FeedTranslationEntity(int feedId, string languageCode, string translatedDescription)
    {
        FeedId = feedId;
        LanguageCode = languageCode;
        TranslatedDescription = translatedDescription;
    }

    public int FeedId { get; set; } // Reference to Feed.Id

    public string LanguageCode { get; set; } // NOT NULL

    public string TranslatedDescription { get; set; } // NOT NULL

    public FeedEntity Feed { get; set; }
}