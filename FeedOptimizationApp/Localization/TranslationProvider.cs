using FeedOptimizationApp.Services.Interfaces;
using System.ComponentModel;

namespace FeedOptimizationApp.Localization
{
    public class TranslationProvider : INotifyPropertyChanged
    {
        private readonly ITranslationService _translationService;
        private string _currentLanguageCode = "en";
        private Dictionary<string, string> _translations = new();

        public static TranslationProvider Instance { get; private set; }

        /// <summary>
        /// Flag to prevent recursive updates when setting language programmatically.
        /// </summary>
        public bool IsUpdatingLanguage { get; private set; }

        public TranslationProvider(ITranslationService translationService)
        {
            _translationService = translationService ?? throw new ArgumentNullException(nameof(translationService));
            Instance = this;
            LoadTranslations();
        }

        /// <summary>
        /// Indexer for translation lookup (side-effect free!).
        /// </summary>
        public string this[string key]
        {
            get
            {
                return _translations.TryGetValue(key, out var value) ? value : $"[{key}]";
            }
        }

        public event EventHandler<string>? LanguageChanged;

        public void SetLanguage(string languageCode)
        {
            if (_currentLanguageCode == languageCode)
                return;

            try
            {
                IsUpdatingLanguage = true;

                _currentLanguageCode = languageCode;
                LoadTranslations();

                // Notify subscribers that the language changed
                LanguageChanged?.Invoke(this, _currentLanguageCode);
            }
            finally
            {
                IsUpdatingLanguage = false;
            }
        }

        private void LoadTranslations()
        {
            _translations = _translationService.GetTranslationDictionary(_currentLanguageCode);

            // Only notify a dedicated property, not all bindings
            OnPropertyChanged(nameof(TranslationsUpdated));
        }

        /// <summary>
        /// Dummy property to trigger bindings when translations update.
        /// </summary>
        public bool TranslationsUpdated => true;

        public void RaiseLanguageChanged() => OnPropertyChanged(nameof(TranslationsUpdated));

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}