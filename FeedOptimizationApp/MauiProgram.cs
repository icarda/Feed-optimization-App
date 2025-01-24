using CommunityToolkit.Maui;
using DataLibrary;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules;
using FeedOptimizationApp.Modules.Calculations;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Modules.Legal;
using FeedOptimizationApp.Modules.Settings;
using FeedOptimizationApp.Services;
using FeedOptimizationApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FeedOptimizationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSansRegular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSansSemibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("faregular.ttf", "FaRegular");
                    fonts.AddFont("fasolid.ttf", "FaSolid");
                    fonts.AddFont("fabrands.ttf", "FaBrands");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Register the ApplicationDbContext
            var databasePath = GetDatabasePath();
            if (DoesDatabaseExist(databasePath))
            {
                Console.WriteLine($"Database exists at path: {databasePath}");
            }
            else
            {
                Console.WriteLine($"Database does not exist at path: {databasePath}");
            }

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite($"Filename={GetDatabasePath()}", b => b.MigrationsAssembly("DataLibrary")));

            // Register other services
            builder.Services.AddSingleton<SharedData>();
            builder.Services.AddSingleton<DatabaseInitializer>();
            builder.Services.AddSingleton<DeviceService>();
            builder.Services.AddSingleton<BaseService>();
            builder.Services.AddSingleton<IFeedService, FeedService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<ICalculationService, CalculationService>();
            builder.Services.AddSingleton<IEnumEntitiesService, EnumEntitiesService>();

            // Register view models
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<LegalViewModel>();
            builder.Services.AddTransient<CreateCalculationViewModel>();
            builder.Services.AddTransient<ViewCalculationsViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();

            // Register views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<LegalPage>();
            builder.Services.AddSingleton<CreateCalculationPage>();
            builder.Services.AddSingleton<ViewCalculationsPage>();
            builder.Services.AddSingleton<SettingsPage>();

            return builder.Build();
        }

        public static string GetDatabasePath()
        {
            var databasePath = "";
            var databaseName = "FeedOptimization.db3";

            databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);

            return databasePath;
        }

        public static bool DoesDatabaseExist(string databasePath)
        {
            return File.Exists(databasePath);
        }
    }
}