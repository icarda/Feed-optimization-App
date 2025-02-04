using DataLibrary.DTOs;
using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Legal;

public class LegalViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly BaseService _baseService;

    public ICommand BackCommand { get; }
    public ICommand AgreeCommand { get; }

    public LegalViewModel(BaseService baseService, SharedData sharedData)
        : base(sharedData)
    {
        _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        BackCommand = new Command(OnBackButtonClicked);
        AgreeCommand = new Command(async () => await OnAgreeButtonClicked());
    }

    private bool _hasAgreed;

    public bool HasAgreed
    {
        get => _hasAgreed;
        set => SetProperty(ref _hasAgreed, value);
    }

    private void OnBackButtonClicked()
    {
        // Handle the Back button click event
        // For example, navigate to the previous page
        Application.Current.MainPage.Navigation.PopAsync();
    }

    private async Task OnAgreeButtonClicked()
    {
        if (HasAgreed)
        {
            try
            {
                var userEntity = new UserEntity
                {
                    CountryId = SharedData.SelectedCountry.Id,
                    LanguageId = SharedData.SelectedLanguage.Id,
                    SpeciesId = SharedData.SelectedSpecies.Id,
                    TermsAndConditions = true,
                    CreatedAt = DateTime.UtcNow,
                    DeviceManufacturer = DeviceInfo.Manufacturer,
                    DeviceModel = DeviceInfo.Model,
                    DeviceName = DeviceInfo.Name,
                    DeviceVersionString = DeviceInfo.VersionString,
                    DevicePlatform = DeviceInfo.Platform.ToString(),
                    DeviceIdiom = DeviceInfo.Idiom.ToString(),
                    DeviceType = DeviceInfo.DeviceType.ToString()

                    // Add other device details here
                };

                await _baseService.UserService.SaveAsync(userEntity);

                var homeViewModel = new HomeViewModel(_baseService, SharedData);
                var newHomePage = new AppShell
                {
                    BindingContext = homeViewModel
                };

                Application.Current.MainPage = newHomePage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Please agree to the terms to continue.", "OK");
        }
    }
}