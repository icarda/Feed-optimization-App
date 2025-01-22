using System.ComponentModel.DataAnnotations;

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

    public int FeedId { get; private set; } // Reference to Feed.Id

    public string LanguageCode { get; private set; } // NOT NULL

    public string TranslatedDescription { get; private set; } // NOT NULL

    public FeedEntity Feed { get; set; }
}