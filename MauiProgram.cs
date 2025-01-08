using FeedOptimizationApp.Database;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddSingleton<MainPage>(); //only because of test on main page, can also remove ApplicationDbContext from page later

            return builder.Build();
        }
    }
}