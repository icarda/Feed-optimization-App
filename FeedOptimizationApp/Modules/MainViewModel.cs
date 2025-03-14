using DataLibrary.Models.Enums;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
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

        /// <summary>
        /// Command for the Next button.
        /// </summary>
        public ICommand NextCommand { get; }

        /// <summary>
        /// Command for Picker selection change.
        /// </summary>
        public ICommand PickerSelectionChangedCommand { get; }

        /// <summary>
        /// Gets or sets the collection of languages.
        /// </summary>
        public ObservableCollection<LanguageEntity> Languages { get; set; } = new ObservableCollection<LanguageEntity>();

        /// <summary>
        /// Gets or sets the collection of countries.
        /// </summary>
        public ObservableCollection<CountryEntity> Countries { get; set; } = new ObservableCollection<CountryEntity>();

        /// <summary>
        /// Gets or sets the collection of species.
        /// </summary>
        public ObservableCollection<SpeciesEntity> SpeciesList { get; set; } = new ObservableCollection<SpeciesEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="baseService">The base service.</param>
        /// <param name="sharedData">The shared data.</param>
        public MainViewModel(BaseService baseService, SharedData sharedData)
            : base(sharedData)
        {
            _baseService = baseService;
            LoadEnumValues();
            NextCommand = new Command(OnNextButtonClicked);
        }

        private bool _isLanguageSelected = false;

        /// <summary>
        /// Gets or sets a value indicating whether the language is selected.
        /// </summary>
        public bool IsLanguageSelected
        {
            get => _isLanguageSelected;
            set => SetProperty(ref _isLanguageSelected, value);
        }

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        public LanguageEntity? SelectedLanguage
        {
            get => SharedData.SelectedLanguage;
            set
            {
                if (SharedData.SelectedLanguage != value)
                {
                    SharedData.SelectedLanguage = value;
                    Console.WriteLine($"SelectedLanguage changed to: {value}"); // Log the change
                    IsLanguageSelected = !string.IsNullOrEmpty(value?.ToString()); // Update IsLanguageSelected
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected country.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the selected species.
        /// </summary>
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

        /// <summary>
        /// Handles the Next button click event.
        /// Navigates to the LegalPage and clears the navigation stack.
        /// </summary>
        private async void OnNextButtonClicked()
        {
            if (SelectedCountry != null &&
                SelectedLanguage != null &&
                SelectedSpecies != null)
            {
                // Save the selections to SharedData
                SharedData.SelectedCountry = SelectedCountry;
                SharedData.SelectedLanguage = SelectedLanguage;
                SharedData.SelectedSpecies = SelectedSpecies;

                var databaseInitializer = App.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                await databaseInitializer.ImportFeedsFromEmbeddedCsvAsync(
                    SelectedCountry.Id,
                    SelectedLanguage.Id
                );

                // Navigate to the LegalPage
                var viewModel = new LegalViewModel(_baseService, SharedData);
                if (Application.Current != null && Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new NavigationPage(new LegalPage(viewModel));
                }
            }
            else
            {
                // Show a message to the user to select all the options
                await Application.Current.MainPage.DisplayAlert("Error", "Please select all the options to continue.", "OK");
            }
        }

        /// <summary>
        /// Loads enumeration values for languages, countries, and species.
        /// </summary>
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
    }
}