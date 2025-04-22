namespace FeedOptimizationApp.Services.Interfaces
{
    public interface ITranslationService
    {
        /// <summary>
        /// Sets the active language for translations.
        /// </summary>
        /// <param name="languageCode">The language code (e.g., "en", "fr").</param>
        void SetLanguage(string languageCode);

        /// <summary>
        /// Retrieves the translated string for a given key.
        /// </summary>
        /// <param name="key">The translation key (e.g., "FeedOptimizationApp_Title").</param>
        string GetTranslation(string key);

        Dictionary<string, string> GetTranslationDictionary(string languageCode);
    }
}