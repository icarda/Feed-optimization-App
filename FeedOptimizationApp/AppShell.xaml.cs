using FeedOptimizationApp.Modules.Calculations;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Modules.Settings;

namespace FeedOptimizationApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CreateCalculationPage), typeof(CreateCalculationPage));
            Routing.RegisterRoute(nameof(ViewCalculationsPage), typeof(ViewCalculationsPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}