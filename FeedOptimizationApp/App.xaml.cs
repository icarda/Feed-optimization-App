using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules;
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
            var viewModel = new MainViewModel(baseService, sharedData);
            MainPage = new NavigationPage(new MainPage(viewModel));
        }
    }
}