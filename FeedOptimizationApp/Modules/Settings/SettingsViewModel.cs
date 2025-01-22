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

public class SettingsViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly BaseService _baseService;

    public ObservableCollection<LookupDTO> Languages { get; set; } = new ObservableCollection<LookupDTO>();
    public ObservableCollection<LookupDTO> Countries { get; set; } = new ObservableCollection<LookupDTO>();
    public ObservableCollection<LookupDTO> SpeciesList { get; set; } = new ObservableCollection<LookupDTO>();

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
            /*var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                RefCountryId = Guid.NewGuid(), // Replace with actual data
                RefLanguageId = Guid.NewGuid(), // Replace with actual data
                RefSpeciesId = Guid.NewGuid(), // Replace with actual data
                TermsAndConditions = false,
                CreatedAt = DateTime.UtcNow
            };*/

            try
            {
                //await _baseService.UserService.SaveUserWithDeviceDetails(user);
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
}