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
            var databaseInitializer = serviceProvider.GetRequiredService<DatabaseInitializer>();
            databaseInitializer.InitializeAsync().Wait();
            var translationProvider = serviceProvider.GetRequiredService<TranslationProvider>();
            translationProvider.RaiseLanguageChanged();
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