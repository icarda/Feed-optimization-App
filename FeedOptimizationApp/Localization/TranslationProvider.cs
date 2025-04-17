using FeedOptimizationApp.Services.Interfaces;
using System.ComponentModel;

namespace FeedOptimizationApp.Localization;

public class TranslationProvider : INotifyPropertyChanged
{
    private readonly ITranslationService _translationService;

    public TranslationProvider(ITranslationService translationService)
    {
        _translationService = translationService;
        RaiseLanguageChanged(); // load initial translations
    }

    private string _currentLanguageCode = "en";
    private Dictionary<string, string> _translations = new();

    public event PropertyChangedEventHandler PropertyChanged;

    public string this[string key]
    {
        get
        {
            var value = _translations.ContainsKey(key) ? _translations[key] : key;
            return value;
        }
    }

    public void RaiseLanguageChanged()
    {
        _translations = _translationService.GetTranslationDictionary(_currentLanguageCode);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    public void SetLanguage(string languageCode)
    {
        _currentLanguageCode = languageCode;
        RaiseLanguageChanged();
    }
}