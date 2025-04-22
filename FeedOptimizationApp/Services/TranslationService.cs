using FeedOptimizationApp.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

public class TranslationService : ITranslationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations = new();
    private string _currentLanguageCode = "en";

    public void SetLanguage(string languageCode)
    {
        if (!_translations.ContainsKey(languageCode))
        {
            var translationDict = LoadTranslationFile(languageCode);

            if (translationDict != null)
            {
                _translations[languageCode] = translationDict;
            }
            else
            {
                languageCode = "en";
                if (!_translations.ContainsKey("en"))
                {
                    _translations["en"] = LoadTranslationFile("en") ?? new();
                }
            }
        }

        _currentLanguageCode = languageCode;
    }

    public string GetTranslation(string key)
    {
        if (_translations.TryGetValue(_currentLanguageCode, out var languageDict) &&
            languageDict.TryGetValue(key, out var translation))
        {
            return translation;
        }

        // Fallback to English
        if (_translations.TryGetValue("en", out var englishDict) &&
            englishDict.TryGetValue(key, out var englishTranslation))
        {
            return englishTranslation;
        }

        Console.WriteLine($"[TranslationService] No translation found for key '{key}'. Returning key as fallback.");
        return key; // Last resort
    }

    public Dictionary<string, string> GetTranslationDictionary(string languageCode)
    {
        if (!_translations.ContainsKey(languageCode))
        {
            _translations[languageCode] = LoadTranslationFile(languageCode) ?? new();
        }

        return _translations[languageCode];
    }

    private Dictionary<string, string>? LoadTranslationFile(string languageCode)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"FeedOptimizationApp.Resources.Translations.{languageCode}.json";

            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return null;
            }

            // Explicitly specify UTF-8 encoding to handle special characters like accents
            using StreamReader reader = new(stream, System.Text.Encoding.UTF8);
            string json = reader.ReadToEnd();

            // Deserialize the JSON content into a dictionary
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[TranslationService] Error loading translation for {languageCode}: {ex.Message}");
            return null;
        }
    }
}