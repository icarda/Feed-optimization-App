using DataLibrary.DTOs;
using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Settings;

public class SettingsViewModel : BaseViewModel
{
    private readonly BaseService _baseService;

    public ObservableCollection<LanguageEntity> Languages { get; set; } = new ObservableCollection<LanguageEntity>();
    public ObservableCollection<CountryEntity> Countries { get; set; } = new ObservableCollection<CountryEntity>();
    public ObservableCollection<SpeciesEntity> SpeciesList { get; set; } = new ObservableCollection<SpeciesEntity>();

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

    public ICommand CancelCommand { get; }
    public ICommand SaveCommand { get; }

    public SettingsViewModel(BaseService baseService, SharedData sharedData)
        : base(sharedData)
    {
        _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        //GetDropDownNameValuesAsync();
        LoadEnumValuesAsync();
        CancelCommand = new Command(OnCancelButtonClicked);
        SaveCommand = new Command(async () => await OnSaveButtonClicked());
    }

    private void OnCancelButtonClicked()
    {
        // Handle the Cancel button click event
        // For example, navigate to the previous page
        Application.Current.MainPage.Navigation.PopAsync();
    }

    private async Task OnSaveButtonClicked()
    {
        // Handle the Save button click event
        if (SelectedLanguage != null && SelectedCountry != null && SelectedSpecies != null)
        {
            try
            {
                var userResult = await _baseService.UserService.GetAllAsync();
                var user = userResult.Data.FirstOrDefault();

                if (user != null)
                {
                    // Update existing user
                    user.CountryId = SelectedCountry.Id;
                    user.LanguageId = SelectedLanguage.Id;
                    user.SpeciesId = SelectedSpecies.Id;

                    await _baseService.UserService.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
        else
        {
            // Show a message to the user to fill all fields
            await Application.Current.MainPage.DisplayAlert("Error", "Please fill all fields to proceed.", "OK");
        }
    }

    private async Task GetDropDownNameValuesAsync()
    {
        try
        {
            if (SelectedLanguage != null)
            {
                var languageResult = await _baseService.EnumEntitiesService.GetLanguageByIdAsync(SelectedLanguage.Id);
                if (languageResult.Succeeded)
                {
                    SelectedLanguage = languageResult.Data;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }

            if (SelectedCountry != null)
            {
                var countryResult = await _baseService.EnumEntitiesService.GetCountryByIdAsync(SelectedCountry.Id);
                if (countryResult.Succeeded)
                {
                    SelectedCountry = countryResult.Data;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }

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
            Debug.WriteLine($"Error in GetDropDownNameValues: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to load dropdown values.", "OK");
        }
    }

    private async Task LoadEnumValuesAsync()
    {
        try
        {
            // Start tasks in parallel
            var languageTask = _baseService.EnumEntitiesService.GetLanguagesAsync();
            var countryTask = _baseService.EnumEntitiesService.GetCountriesAsync();
            var speciesTask = _baseService.EnumEntitiesService.GetSpeciesAsync();

            // Process results for languages
            if (languageTask.Result.Succeeded)
            {
                Languages.Clear();
                foreach (var language in languageTask.Result.Data)
                {
                    Languages.Add(language);
                }
            }

            // Process results for countries
            if (countryTask.Result.Succeeded)
            {
                Countries.Clear();
                foreach (var country in countryTask.Result.Data)
                {
                    Countries.Add(country);
                }
            }

            // Process results for species
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
            Debug.WriteLine($"Error in LoadEnumValuesAsync: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to load dropdown values.", "OK");
        }
    }
}