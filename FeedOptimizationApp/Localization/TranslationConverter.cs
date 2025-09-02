using DataLibrary.Models;
using DataLibrary.Models.Enums;
using DataLibrary.Seedwork;
using System.ComponentModel;
using System.Globalization;

namespace FeedOptimizationApp.Localization
{
    public class TranslationConverter : BindableObject, IValueConverter
    {
        public TranslationProvider TranslationProvider
        {
            get => (TranslationProvider)GetValue(TranslationProviderProperty);
            set => SetValue(TranslationProviderProperty, value);
        }

        public static readonly BindableProperty TranslationProviderProperty =
            BindableProperty.Create(nameof(TranslationProvider), typeof(TranslationProvider), typeof(TranslationConverter));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            string key = value switch
            {
                LanguageEntity lang => $"Language_{lang.Name}",
                CountryEntity country => $"Country_{country.Name}",
                SpeciesEntity species => $"Species_{species.Name}",
                DietQualityEstimateEntity dietQuality => $"DietQualityEstimate_{dietQuality.Name}",
                GoatTypeEntity goatType => $"GoatType_{goatType.Name}",
                GrazingEntity grazing => $"Grazing_{grazing.Name}",
                SheepTypeEntity sheepType => $"SheepType_{sheepType.Name}",
                Enumeration enumEntity => enumEntity.Name,
                string str => str,
                _ => value.ToString()!
            };

            return TranslationProvider?[key] ?? key;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }


    public class LanguageToCodeConverter
    {
        public string Convert(LanguageEntity language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            return LanguageCodeMapper.ToCode(language);
        }
    }

    public class TranslatedItem<T> : INotifyPropertyChanged
    {
        public T Value { get; }
        private readonly Func<string> _getLabel;

        private string _label;
        public string Label
        {
            get => _label;
            private set
            {
                if (_label != value)
                {
                    _label = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
                }
            }
        }

        public TranslatedItem(T value, Func<string> getLabel)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            _getLabel = getLabel ?? throw new ArgumentNullException(nameof(getLabel));
            Label = _getLabel();
        }

        /// <summary>
        /// Call this when language changes.
        /// </summary>
        public void Refresh()
        {
            Label = _getLabel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    

}
