using FeedOptimizationApp.Services.Interfaces;
using System.ComponentModel;

namespace FeedOptimizationApp.Localization
{
    public class TranslationProvider : INotifyPropertyChanged
    {
        private readonly ITranslationService _translationService;
        private string _currentLanguageCode = "en";
        private Dictionary<string, string> _translations = new();

        public TranslationProvider(ITranslationService translationService)
        {
            _translationService = translationService;
            LoadTranslations();
        }

        public string this[string key] => _translations.ContainsKey(key) ? _translations[key] : key;

        public void SetLanguage(string languageCode)
        {
            if (_currentLanguageCode != languageCode)
            {
                _currentLanguageCode = languageCode;
                LoadTranslations();
                RaiseLanguageChanged();
            }
        }

        private void LoadTranslations()
        {
            _translations = _translationService.GetTranslationDictionary(_currentLanguageCode);

            OnPropertyChanged(null); // Notify all bindings to update
        }

        public void RaiseLanguageChanged() => OnPropertyChanged(null);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}