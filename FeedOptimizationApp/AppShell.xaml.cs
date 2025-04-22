using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Modules.Calculations;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Modules.Settings;

namespace FeedOptimizationApp
{
    public partial class AppShell : Shell
    {
        private readonly TranslationProvider _translationProvider;

        public AppShell(TranslationProvider translationProvider)
        {
            InitializeComponent();
            _translationProvider = translationProvider;

            // Listen for language changes to update tab titles dynamically
            _translationProvider.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == null)
                {
                    OnPropertyChanged(nameof(Tab_Home));
                    OnPropertyChanged(nameof(Tab_Create));
                    OnPropertyChanged(nameof(Tab_View));
                    OnPropertyChanged(nameof(Tab_Settings));
                }
            };

            BindingContext = this;

            // Register routes for navigation
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CreateCalculationPage), typeof(CreateCalculationPage));
            Routing.RegisterRoute(nameof(ViewCalculationsPage), typeof(ViewCalculationsPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

        // Properties for tab titles bound to the TranslationProvider
        public string Tab_Home => _translationProvider["Tab_Home"];

        public string Tab_Create => _translationProvider["Tab_Create"];
        public string Tab_View => _translationProvider["Tab_View"];
        public string Tab_Settings => _translationProvider["Tab_Settings"];
    }
}