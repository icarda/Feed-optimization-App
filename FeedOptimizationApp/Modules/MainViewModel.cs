using DataLibrary.Models.Enums;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Modules.Legal;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules
{
    /// <summary>
    /// ViewModel for the main page.
    /// </summary>
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly BaseService _baseService;

        public ICommand NextCommand { get; }

        

        public MainViewModel(BaseService baseService, SharedData sharedData, TranslationProvider translationProvider)
            : base(sharedData, translationProvider)
        {
            _baseService = baseService;

            // Listen for language changes
            SharedData.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SharedData.SelectedLanguage))
                {
                    if (SharedData.SelectedLanguage != null)
                    {
                        var languageCode = SharedData.SelectedLanguage.Id == 1 ? "en" : "fr";
                        TranslationProvider.SetLanguage(languageCode);
                    }
                    else
                    {
                        // Handle the case where SelectedLanguage is null (e.g., first use)
                        IsLanguageSelected = false;
                    }
                }
            };

            LoadEnumValues();
            NextCommand = new Command(OnNextButtonClicked);

            translationProvider.LanguageChanged += (s, e) => RefreshTranslations();
        }

        private void RefreshTranslations()
        {
            foreach (var item in Languages) item.Refresh();
            Languages = new ObservableCollection<TranslatedItem<LanguageEntity>>(Languages);
            OnPropertyChanged(nameof(Languages));

            foreach (var item in Countries) item.Refresh();
            Countries = new ObservableCollection<TranslatedItem<CountryEntity>>(Countries);
            OnPropertyChanged(nameof(Countries));

            foreach (var item in SpeciesList) item.Refresh();
            SpeciesList = new ObservableCollection<TranslatedItem<SpeciesEntity>>(SpeciesList);
            OnPropertyChanged(nameof(SpeciesList));

            OnPropertyChanged(nameof(SelectedLanguage));
            OnPropertyChanged(nameof(SelectedCountry));
            OnPropertyChanged(nameof(SelectedSpecies));
        }

        private bool _isLanguageSelected = false;

        public bool IsLanguageSelected
        {
            get => _isLanguageSelected;
            set => SetProperty(ref _isLanguageSelected, value);
        }

        public ObservableCollection<TranslatedItem<LanguageEntity>> Languages { get; set; } = new();
        public ObservableCollection<TranslatedItem<CountryEntity>> Countries { get; set; } = new();
        public ObservableCollection<TranslatedItem<SpeciesEntity>> SpeciesList { get; set; } = new();

        private TranslatedItem<LanguageEntity>? _selectedLanguage;
        public TranslatedItem<LanguageEntity>? SelectedLanguage
        {
            get => Languages.FirstOrDefault(x => x.Value.Id == SharedData.SelectedLanguage?.Id);
            set
            {
                if (value != null && SharedData.SelectedLanguage?.Id != value.Value.Id)
                {
                    SharedData.SelectedLanguage = value.Value; // <-- .Value here
                    var langCode = LanguageCodeMapper.ToCode(value.Value);
                    TranslationProvider.SetLanguage(langCode);
                    IsLanguageSelected = true;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        private TranslatedItem<CountryEntity>? _selectedCountry;
        public TranslatedItem<CountryEntity>? SelectedCountry
        {
            get => Countries.FirstOrDefault(x => x.Value.Id == SharedData.SelectedCountry?.Id);
            set
            {
                if (value != null && SharedData.SelectedCountry?.Id != value.Value.Id)
                {
                    SharedData.SelectedCountry = value.Value; // <-- .Value here
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        private TranslatedItem<SpeciesEntity>? _selectedSpecies;
        public TranslatedItem<SpeciesEntity>? SelectedSpecies
        {
            get => SpeciesList.FirstOrDefault(x => x.Value.Id == SharedData.SelectedSpecies?.Id);
            set
            {
                if (value != null && SharedData.SelectedSpecies?.Id != value.Value.Id)
                {
                    SharedData.SelectedSpecies = value.Value; // <-- .Value here
                    OnPropertyChanged(nameof(SelectedSpecies));
                }
            }
        }

        private async void OnNextButtonClicked()
        {
            if (SelectedCountry != null &&
                SelectedLanguage != null &&
                SelectedSpecies != null)
            {
                SharedData.SelectedCountry = SelectedCountry?.Value;
                SharedData.SelectedLanguage = SelectedLanguage?.Value;
                SharedData.SelectedSpecies = SelectedSpecies?.Value;

                var databaseInitializer = App.ServiceProvider.GetRequiredService<DatabaseInitializer>();

                var feeds = await _baseService.FeedService.GetAllAsync();
                if (feeds.Data.Count > 0)
                {
                    await databaseInitializer.ClearFeedsAsync();
                }

                await databaseInitializer.ImportFeedsFromEmbeddedCsvAsync(
                    SelectedCountry.Value.Id,
                    SelectedLanguage.Value.Id
                );

                var viewModel = new LegalViewModel(_baseService, SharedData, databaseInitializer, TranslationProvider);
                if (Application.Current != null && Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new NavigationPage(new LegalPage(viewModel));
                }
            }
            else
            {
                var errorTitle = TranslationProvider["MainPage_ErrorTitle"];
                var errorMessage = TranslationProvider["MainPage_Error_SelectAll"];
                var okButtonText = TranslationProvider["MainPage_OKButton"];
                await Application.Current.MainPage.DisplayAlert(errorTitle, errorMessage, okButtonText);
            }
        }

        private async void LoadEnumValues()
        {
            Languages.Clear();
            Countries.Clear();
            SpeciesList.Clear();

            var languages = await _baseService.EnumEntitiesService.GetLanguagesAsync();
            foreach (var language in languages.Data)
            {
                Languages.Add(new TranslatedItem<LanguageEntity>(language, () => TranslationProvider[$"Language_{language.Name}"]));
            }

            var countries = await _baseService.EnumEntitiesService.GetCountriesAsync();
            foreach (var country in countries.Data)
            {
                Countries.Add(new TranslatedItem<CountryEntity>(country, () => TranslationProvider[$"Country_{country.Name}"]));
            }

            var speciesList = await _baseService.EnumEntitiesService.GetSpeciesAsync();
            foreach (var species in speciesList.Data)
            {
                SpeciesList.Add(new TranslatedItem<SpeciesEntity>(species, () => TranslationProvider[$"Species_{species.Name}"]));
            }
        }
    }
}