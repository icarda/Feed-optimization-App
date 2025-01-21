using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Settings;

public class SettingsViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly BaseService _baseService;

    private string _selectedLanguage;
    private string _selectedCountry;
    private string _selectedSpecies;

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set => SetProperty(ref _selectedLanguage, value);
    }

    public string SelectedCountry
    {
        get => _selectedCountry;
        set => SetProperty(ref _selectedCountry, value);
    }

    public string SelectedSpecies
    {
        get => _selectedSpecies;
        set => SetProperty(ref _selectedSpecies, value);
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
        if (!string.IsNullOrEmpty(SelectedLanguage) && !string.IsNullOrEmpty(SelectedCountry) && !string.IsNullOrEmpty(SelectedSpecies))
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