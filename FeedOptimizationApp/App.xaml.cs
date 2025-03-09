using DataLibrary.DTOs;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;

namespace FeedOptimizationApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            ServiceProvider = serviceProvider;
            var databaseInitializer = serviceProvider.GetRequiredService<DatabaseInitializer>();
            databaseInitializer.InitializeAsync().Wait();
            var sharedData = serviceProvider.GetRequiredService<SharedData>();
            var baseService = serviceProvider.GetRequiredService<BaseService>();

            // Check if user selections exist in the database
            var userResult = baseService?.UserService?.GetAllAsync().Result;
            var user = userResult?.Data?.FirstOrDefault();

            if (user != null && sharedData != null && baseService != null)
            {
                // Load user selections into SharedData
                sharedData.SelectedCountry = ConversionHelpers.ConvertToCountryEntity(user.CountryId);
                sharedData.SelectedLanguage = ConversionHelpers.ConvertToLanguageEntity(user.LanguageId);
                sharedData.SelectedSpecies = ConversionHelpers.ConvertToSpeciesEntity(user.SpeciesId);

                // Navigate to the main page
                var vm = new MainViewModel(baseService, sharedData);
                MainPage = new AppShell();

                var viewModel = new HomeViewModel(baseService, sharedData);
                // Assuming HomePage is part of AppShell, no need to push it onto the navigation stack
            }
            else
            {
                // Navigate to the initial setup page
                var viewModel = new MainViewModel(baseService, sharedData);
                MainPage = new NavigationPage(new MainPage(viewModel));
            }
        }
    }
}