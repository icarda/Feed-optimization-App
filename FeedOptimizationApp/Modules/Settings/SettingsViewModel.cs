using DataLibrary.Models.Enums;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using DataLibrary.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;

namespace FeedOptimizationApp.Modules.Settings
{
    /// <summary>
    /// ViewModel for managing the application settings.
    /// Handles loading and saving user preferences for language, country, and species.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        // Service for performing data operations.
        private readonly BaseService _baseService;

        // Service for initializing the database.
        private readonly DatabaseInitializer _databaseInitializer;

        // Observable collections to hold dropdown options.
        public ObservableCollection<LanguageEntity> Languages { get; set; } = new ObservableCollection<LanguageEntity>();

        public ObservableCollection<CountryEntity> Countries { get; set; } = new ObservableCollection<CountryEntity>();
        public ObservableCollection<SpeciesEntity> SpeciesList { get; set; } = new ObservableCollection<SpeciesEntity>();

        // Private fields to store initial values.
        private LanguageEntity? _initialSelectedLanguage;

        private CountryEntity? _initialSelectedCountry;
        private SpeciesEntity? _initialSelectedSpecies;

        // Flag to track if the save button was clicked.
        private bool _isSaveButtonClicked;

        // Flag to track if the selections have changed.
        private bool _selectionsChanged;

        /// <summary>
        /// Gets or sets the selected language.
        /// Uses shared data to persist the selection.
        /// </summary>
        public LanguageEntity? SelectedLanguage
        {
            get => SharedData.SelectedLanguage;
            set
            {
                if (SharedData.SelectedLanguage != value)
                {
                    SharedData.SelectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                    _selectionsChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected country.
        /// Uses shared data to persist the selection.
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
                    _selectionsChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected species.
        /// Uses shared data to persist the selection.
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

        // Commands for canceling and saving settings.
        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        /// <summary>
        /// Initializes a new instance of the SettingsViewModel.
        /// Loads available languages, countries, and species, and sets up commands.
        /// </summary>
        /// <param name="baseService">Service for accessing data.</param>
        /// <param name="sharedData">Shared data context across the application.</param>
        /// <param name="databaseInitializer">Service for initializing the database.</param>
        public SettingsViewModel(BaseService baseService, SharedData sharedData, DatabaseInitializer databaseInitializer)
            : base(sharedData)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _databaseInitializer = databaseInitializer ?? throw new ArgumentNullException(nameof(databaseInitializer));

            // Store initial values.
            _initialSelectedLanguage = sharedData.SelectedLanguage;
            _initialSelectedCountry = sharedData.SelectedCountry;
            _initialSelectedSpecies = sharedData.SelectedSpecies;

            // Load dropdown options for language, country, and species.
            LoadEnumValuesAsync();

            // Initialize the Cancel and Save commands.
            CancelCommand = new Command(OnCancelButtonClicked);
            SaveCommand = new Command(async () => await OnSaveButtonClicked());
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// Resets the dropdowns to their initial values.
        /// </summary>
        private void OnCancelButtonClicked()
        {
            // Reset the selected items to their initial values.
            SelectedLanguage = _initialSelectedLanguage;
            SelectedCountry = _initialSelectedCountry;
            SelectedSpecies = _initialSelectedSpecies;
        }

        /// <summary>
        /// Handles the Save button click event.
        /// Validates that all required fields are filled and updates the user's settings.
        /// </summary>
        private async Task OnSaveButtonClicked()
        {
            // Ensure all required settings have been selected.
            if (SelectedLanguage != null && SelectedCountry != null && SelectedSpecies != null)
            {
                // Show the custom alert popup to confirm the action.
                var popup = new CustomAlertPopup(
                    "Save user settings",
                    "Warning: You might lose stored calculation data. Do you want to continue?",
                    async () =>
                    {
                        try
                        {
                            // Retrieve the current user (assumes a single user record exists in the database).
                            var userResult = await _baseService.UserService.GetAllAsync();
                            var user = userResult.Data.FirstOrDefault();

                            if (user != null)
                            {
                                // Update the user's settings with the selected values.
                                user.CountryId = SelectedCountry.Id;
                                user.LanguageId = SelectedLanguage.Id;
                                user.SpeciesId = SelectedSpecies.Id;

                                // Save the updated user entity to the database.
                                await _baseService.UserService.UpdateAsync(user);

                                // Update the initial values to reflect the saved selections.
                                _initialSelectedLanguage = SelectedLanguage;
                                _initialSelectedCountry = SelectedCountry;
                                _initialSelectedSpecies = SelectedSpecies;

                                // Update the shared data to reflect the saved selections.
                                SharedData.SelectedLanguage = SelectedLanguage;
                                SharedData.SelectedCountry = SelectedCountry;
                                SharedData.SelectedSpecies = SelectedSpecies;

                                // Set the flag to indicate that the save button was clicked.
                                _isSaveButtonClicked = true;

                                // If selections have changed, clear and repopulate the FeedEntity table.
                                if (_selectionsChanged)
                                {
                                    // Show a toast message to indicate that changes are being saved.
                                    var toast = Toast.Make("Saving changes...");
                                    await toast.Show();

                                    // Trigger the reset event to clear any dependent UI components (e.g., pickers).
                                    _baseService.ResetPickerService.ResetPicker();
                                    Console.WriteLine("Picker reset triggered from SettingsViewModel.");

                                    // Clear all calculations and repopulate the feeds table with the new settings.
                                    await _databaseInitializer.ClearAllCalculationsAsync();
                                    await _databaseInitializer.ClearAndRepopulateFeedsAsync(SelectedCountry.Id, SelectedLanguage.Id);

                                    // Dismiss the toast message after the operation is complete.
                                    await toast.Dismiss();
                                }

                                // Show a toast message to notify the user that the settings were saved successfully.
                                await Toast.Make("User settings saved successfully.", ToastDuration.Long).Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log any exceptions that occur during the update process.
                            Debug.WriteLine($"Exception: {ex.Message}");
                        }
                    });

                // Display the confirmation popup to the user.
                Application.Current.MainPage.ShowPopup(popup);
            }
            else
            {
                // Notify the user to complete all fields if any selection is missing.
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields to proceed.", "OK");
            }
        }

        /// <summary>
        /// (Optional) Retrieves detailed values for the selected dropdowns.
        /// This method is currently not used but can be enabled if needed.
        /// </summary>
        private async Task GetDropDownNameValuesAsync()
        {
            try
            {
                // Update SelectedLanguage details if one is already selected.
                if (SelectedLanguage != null)
                {
                    var languageResult = await _baseService.EnumEntitiesService.GetLanguageByIdAsync(SelectedLanguage.Id);
                    if (languageResult.Succeeded)
                    {
                        SelectedLanguage = languageResult.Data;
                        OnPropertyChanged(nameof(SelectedLanguage));
                    }
                }

                // Update SelectedCountry details if one is already selected.
                if (SelectedCountry != null)
                {
                    var countryResult = await _baseService.EnumEntitiesService.GetCountryByIdAsync(SelectedCountry.Id);
                    if (countryResult.Succeeded)
                    {
                        SelectedCountry = countryResult.Data;
                        OnPropertyChanged(nameof(SelectedCountry));
                    }
                }

                // Update SelectedSpecies details if one is already selected.
                if (SelectedSpecies != null)
                {
                    var speciesResult = await _baseService.EnumEntitiesService.GetSpeciesByIdAsync(SelectedSpecies.Id);
                    if (speciesResult.Succeeded)
                    {
                        SelectedSpecies = speciesResult.Data;
                        OnPropertyChanged(nameof(SelectedSpecies));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and display an alert if the dropdown values fail to load.
                Debug.WriteLine($"Error in GetDropDownNameValues: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load dropdown values.", "OK");
            }
        }

        /// <summary>
        /// Loads the enumeration values for languages, countries, and species asynchronously.
        /// Populates the respective ObservableCollections with the retrieved data.
        /// </summary>
        private async Task LoadEnumValuesAsync()
        {
            try
            {
                // Start tasks to retrieve languages, countries, and species in parallel.
                var languageTask = _baseService.EnumEntitiesService.GetLanguagesAsync();
                var countryTask = _baseService.EnumEntitiesService.GetCountriesAsync();
                var speciesTask = _baseService.EnumEntitiesService.GetSpeciesAsync();

                // Process the results for languages.
                if (languageTask.Result.Succeeded)
                {
                    Languages.Clear();
                    foreach (var language in languageTask.Result.Data)
                    {
                        Languages.Add(language);
                    }
                }

                // Process the results for countries.
                if (countryTask.Result.Succeeded)
                {
                    Countries.Clear();
                    foreach (var country in countryTask.Result.Data)
                    {
                        Countries.Add(country);
                    }
                }

                // Process the results for species.
                if (speciesTask.Result.Succeeded)
                {
                    SpeciesList.Clear();
                    foreach (var species in speciesTask.Result.Data)
                    {
                        SpeciesList.Add(species);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and display an alert if the dropdown values fail to load.
                Debug.WriteLine($"Error in LoadEnumValuesAsync: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load dropdown values.", "OK");
            }
        }

        /// <summary>
        /// Resets the dropdowns to their initial values if the save button was not clicked.
        /// </summary>
        public void OnDisappearing()
        {
            if (!_isSaveButtonClicked)
            {
                // Reset the selected items to their initial values.
                SelectedLanguage = _initialSelectedLanguage;
                SelectedCountry = _initialSelectedCountry;
                SelectedSpecies = _initialSelectedSpecies;
            }
        }
    }
}