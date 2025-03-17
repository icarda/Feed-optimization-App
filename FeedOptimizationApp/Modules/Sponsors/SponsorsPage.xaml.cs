using FeedOptimizationApp.Helpers;
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
            var homeViewModel = new HomeViewModel(baseService, sharedData);
            Application.Current.MainPage = new AppShell(); // Navigate to AppShell
            await Shell.Current.GoToAsync("//HomePage"); // Set the route to HomePage within the shell
        }
        else
        {
            // Navigate to the main page.
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            await Navigation.PushAsync(new MainPage(mainViewModel));
        }
    }
}