using FeedOptimizationApp.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Helpers;

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
        // Handle the Agree button click event
        if (HasAgreed)
        {
            try
            {
                if (Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new AppShell();
                }
                var viewModel = new HomeViewModel(_baseService, SharedData);
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(viewModel));
            }
            catch (NullReferenceException ex)
            {
                // Handle the exception or log it
                Debug.WriteLine($"NullReferenceException: {ex.Message}");
            }
        }
        else
        {
            // Show a message to the user to agree to the terms
            await Application.Current.MainPage.DisplayAlert("Error", "Please agree to the terms to continue.", "OK");
        }
    }
}