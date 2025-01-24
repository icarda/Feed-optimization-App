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
                var userEntity = new UserDTO
                {
                    CountryId = SharedData.SelectedCountry.Id,
                    LanguageId = SharedData.SelectedLanguage.Id,
                    SpeciesId = SharedData.SelectedSpecies.Id,
                    TermsAndConditions = true,
                    CreatedAt = DateTime.UtcNow,
                    // Add other device details here
                };

                var mappedUser = Mappers.MapToUserEntity(userEntity);

                await _baseService.UserService.SaveAsync(mappedUser);

                if (Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new AppShell();
                }
                var viewModel = new HomeViewModel(_baseService, SharedData);
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(viewModel));
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