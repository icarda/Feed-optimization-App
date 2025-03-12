using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;

namespace FeedOptimizationApp.Modules.Sponsors;

public partial class SponsorsPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly bool _navigateToHomePage;

    public SponsorsPage(IServiceProvider serviceProvider, bool navigateToHomePage)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _navigateToHomePage = navigateToHomePage;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(2000); // Wait for 2 seconds

        if (_navigateToHomePage)
        {
            var baseService = _serviceProvider.GetRequiredService<BaseService>();
            var sharedData = _serviceProvider.GetRequiredService<SharedData>();
            var homeViewModel = new HomeViewModel(baseService, sharedData);
            Application.Current.MainPage = new AppShell(); // Navigate to AppShell
            await Shell.Current.GoToAsync("//HomePage"); // Set the route to HomePage within the shell
        }
        else
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            await Navigation.PushAsync(new MainPage(mainViewModel));
        }
    }
}
