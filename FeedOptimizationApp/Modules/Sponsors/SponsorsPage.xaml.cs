using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;
using Microsoft.Maui.Controls.Compatibility;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Add a tap gesture recognizer to navigate to the next page on any click
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += OnScreenTapped;
        MainGrid.GestureRecognizers.Add(tapGestureRecognizer); // Ensure the GestureRecognizers are added to the MainGrid
    }

    private async void OnScreenTapped(object sender, EventArgs e)
    {
        Console.WriteLine("Screen tapped"); // Debug statement
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
