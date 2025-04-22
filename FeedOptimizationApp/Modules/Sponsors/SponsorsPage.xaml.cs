using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;

namespace FeedOptimizationApp.Modules.Sponsors;

public partial class SponsorsPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly bool _navigateToHomePage;

    /// <summary>
    /// Initializes a new instance of the <see cref="SponsorsPage"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider for dependency injection.</param>
    /// <param name="navigateToHomePage">Indicates whether to navigate to the home page after displaying the sponsors page.</param>
    public SponsorsPage(IServiceProvider serviceProvider, bool navigateToHomePage)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _navigateToHomePage = navigateToHomePage;

        // Set the BindingContext to enable translations
        BindingContext = this;

        // Listen for language changes to update translations dynamically
        var translationProvider = _serviceProvider.GetRequiredService<TranslationProvider>();
        translationProvider.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == null)
            {
                OnPropertyChanged(nameof(SponsorsPage_Title));
                OnPropertyChanged(nameof(SponsorsPage_SponsoredBy));
                OnPropertyChanged(nameof(SponsorsPage_CollaborationWith));
            }
        };
    }

    /// <summary>
    /// Called when the page appears.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(2000); // Wait for 2 seconds

        if (_navigateToHomePage)
        {
            // Navigate to the home page using the AppShell and Shell navigation.
            var baseService = _serviceProvider.GetRequiredService<BaseService>();
            var sharedData = _serviceProvider.GetRequiredService<SharedData>();
            var translationProvider = _serviceProvider.GetRequiredService<TranslationProvider>();
            var homeViewModel = new HomeViewModel(baseService, sharedData, translationProvider);
            Application.Current.MainPage = new AppShell(translationProvider); // Navigate to AppShell
            await Shell.Current.GoToAsync("//HomePage"); // Set the route to HomePage within the shell
        }
        else
        {
            // Ensure SelectedLanguage is null for the first use
            var sharedData = _serviceProvider.GetRequiredService<SharedData>();
            sharedData.SelectedLanguage = null;

            // Navigate to the main page.
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            await Navigation.PushAsync(new MainPage(mainViewModel));
        }
    }

    #region TRANSLATIONS

    public string SponsorsPage_Title => _serviceProvider.GetRequiredService<TranslationProvider>()["SponsorsPage_Title"];
    public string SponsorsPage_SponsoredBy => _serviceProvider.GetRequiredService<TranslationProvider>()["SponsorsPage_SponsoredBy"];
    public string SponsorsPage_CollaborationWith => _serviceProvider.GetRequiredService<TranslationProvider>()["SponsorsPage_CollaborationWith"];

    #endregion TRANSLATIONS
}