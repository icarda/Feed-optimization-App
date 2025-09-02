using DataLibrary.Models.Enums;

namespace FeedOptimizationApp.Localization;

public static class LanguageCodeMapper
{
    public static string ToCode(LanguageEntity language)
    {
        return language.Id switch
        {
            1 => "en",
            2 => "fr",
            _ => "en" // default fallback
        };
    }
}