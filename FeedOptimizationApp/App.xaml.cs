using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Modules.Sponsors;
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

            // Initialize the database
            var databaseInitializer = serviceProvider.GetRequiredService<DatabaseInitializer>();
            databaseInitializer.InitializeAsync().Wait();

            // Get required services
            var translationProvider = serviceProvider.GetRequiredService<TranslationProvider>();
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

                // Load the saved language from SharedData and set it in TranslationProvider
                var languageCode = sharedData.SelectedLanguage?.Id == 1 ? "en" : "fr"; // Map language ID to language code
                translationProvider.SetLanguage(languageCode);

                // Navigate to the SponsorsPage
                MainPage = new NavigationPage(new SponsorsPage(serviceProvider, true));
            }
            else
            {
                // Navigate to the SponsorsPage
                MainPage = new NavigationPage(new SponsorsPage(serviceProvider, false));
            }
        }
    }
}