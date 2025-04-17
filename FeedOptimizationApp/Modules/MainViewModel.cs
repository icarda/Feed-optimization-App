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

        public ObservableCollection<LanguageEntity> Languages { get; set; } = new ObservableCollection<LanguageEntity>();
        public ObservableCollection<CountryEntity> Countries { get; set; } = new ObservableCollection<CountryEntity>();
        public ObservableCollection<SpeciesEntity> SpeciesList { get; set; } = new ObservableCollection<SpeciesEntity>();

        public MainViewModel(BaseService baseService, SharedData sharedData, TranslationProvider translationProvider)
            : base(sharedData, translationProvider)
        {
            _baseService = baseService;
            LoadEnumValues();
            NextCommand = new Command(OnNextButtonClicked);

            TranslationProvider.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == null)
                {
                    OnPropertyChanged(nameof(MainPage_Title));
                    OnPropertyChanged(nameof(MainPage_HeadingText));
                    OnPropertyChanged(nameof(MainPage_SelectLanguageLabel));
                    OnPropertyChanged(nameof(MainPage_SelectCountryLabel));
                    OnPropertyChanged(nameof(MainPage_SelectSpeciesLabel));
                    OnPropertyChanged(nameof(MainPage_NextButtonText));
                }
            };
        }

        private bool _isLanguageSelected = false;

        public bool IsLanguageSelected
        {
            get => _isLanguageSelected;
            set => SetProperty(ref _isLanguageSelected, value);
        }

        public LanguageEntity? SelectedLanguage
        {
            get => SharedData.SelectedLanguage;
            set
            {
                if (SharedData.SelectedLanguage != value)
                {
                    SharedData.SelectedLanguage = value;

                    var langCode = LanguageCodeMapper.ToCode(SharedData.SelectedLanguage!);
                    TranslationProvider.SetLanguage(langCode);

                    IsLanguageSelected = value != null;
                    OnPropertyChanged(nameof(SelectedLanguage));

                    OnPropertyChanged(nameof(MainPage_Title));
                    OnPropertyChanged(nameof(MainPage_HeadingText));
                    OnPropertyChanged(nameof(MainPage_SelectLanguageLabel));
                    OnPropertyChanged(nameof(MainPage_SelectCountryLabel));
                    OnPropertyChanged(nameof(MainPage_SelectSpeciesLabel));
                    OnPropertyChanged(nameof(MainPage_NextButtonText));
                }
            }
        }

        public CountryEntity? SelectedCountry
        {
            get => SharedData.SelectedCountry;
            set
            {
                if (SharedData.SelectedCountry != value)
                {
                    SharedData.SelectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        public SpeciesEntity? SelectedSpecies
        {
            get => SharedData.SelectedSpecies;
            set
            {
                if (SharedData.SelectedSpecies != value)
                {
                    SharedData.SelectedSpecies = value;
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
                SharedData.SelectedCountry = SelectedCountry;
                SharedData.SelectedLanguage = SelectedLanguage;
                SharedData.SelectedSpecies = SelectedSpecies;

                var databaseInitializer = App.ServiceProvider.GetRequiredService<DatabaseInitializer>();

                var feeds = await _baseService.FeedService.GetAllAsync();
                if (feeds.Data.Count > 0)
                {
                    await databaseInitializer.ClearFeedsAsync();
                }

                await databaseInitializer.ImportFeedsFromEmbeddedCsvAsync(
                    SelectedCountry.Id,
                    SelectedLanguage.Id
                );

                var viewModel = new LegalViewModel(_baseService, SharedData, databaseInitializer, TranslationProvider);
                if (Application.Current != null && Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new NavigationPage(new LegalPage(viewModel));
                }
            }
            else
            {
                var errorMessage = TranslationProvider["MainPage_Error_SelectAll"];
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
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
                Languages.Add(language);
            }

            var countries = await _baseService.EnumEntitiesService.GetCountriesAsync();
            foreach (var country in countries.Data)
            {
                Countries.Add(country);
            }

            var speciesList = await _baseService.EnumEntitiesService.GetSpeciesAsync();
            foreach (var species in speciesList.Data)
            {
                SpeciesList.Add(species);
            }
        }

        #region TRANSLATIONS

        public string MainPage_Title => TranslationProvider["MainPage_Title"];
        public string MainPage_HeadingText => TranslationProvider["MainPage_Heading"];
        public string MainPage_SelectLanguageLabel => TranslationProvider["MainPage_SelectLanguageLabel"];
        public string MainPage_SelectCountryLabel => TranslationProvider["MainPage_SelectCountryLabel"];
        public string MainPage_SelectSpeciesLabel => TranslationProvider["MainPage_SelectSpeciesLabel"];
        public string MainPage_NextButtonText => TranslationProvider["MainPage_NextButton"];
        public string MainPage_Error_SelectAll => TranslationProvider["MainPage_Error_SelectAll"];

        #endregion TRANSLATIONS
    }
}