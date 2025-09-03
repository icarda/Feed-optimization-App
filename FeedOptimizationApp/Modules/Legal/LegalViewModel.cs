using DataLibrary.Models;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Legal
{
    public class LegalViewModel : BaseViewModel
    {
        private readonly BaseService _baseService;
        private readonly DatabaseInitializer _databaseInitializer;

        public ICommand BackCommand { get; }
        public ICommand AgreeCommand { get; }

        public LegalViewModel(BaseService baseService, SharedData sharedData, DatabaseInitializer databaseInitializer, TranslationProvider translationProvider)
            : base(sharedData, translationProvider)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _databaseInitializer = databaseInitializer ?? throw new ArgumentNullException(nameof(databaseInitializer));

            DisclaimerHtml = _databaseInitializer.LoadDisclaimerFromEmbeddedResource(SharedData.SelectedLanguage!.Id);

            BackCommand = new Command(OnBackButtonClicked);
            AgreeCommand = new Command(async () => await OnAgreeButtonClicked());
        }

        private string _disclaimerHtml;

        public string DisclaimerHtml
        {
            get => _disclaimerHtml;
            set => SetProperty(ref _disclaimerHtml, value);
        }

        private bool _hasAgreed;

        public bool HasAgreed
        {
            get => _hasAgreed;
            set => SetProperty(ref _hasAgreed, value);
        }

        private async void OnBackButtonClicked()
        {
            SharedData.SelectedSpecies = null;
            SharedData.SelectedCountry = null;
            SharedData.SelectedLanguage = null;

            await _databaseInitializer.ClearFeedsAsync();

            Application.Current.MainPage = new MainPage(new MainViewModel(_baseService, SharedData, TranslationProvider));
        }

        private async Task OnAgreeButtonClicked()
        {
            if (HasAgreed)
            {
                try
                {
                    var userEntity = new UserEntity
                    {
                        CountryId = SharedData.SelectedCountry.Id,
                        LanguageId = SharedData.SelectedLanguage.Id,
                        SpeciesId = SharedData.SelectedSpecies.Id,
                        TermsAndConditions = true,
                        CreatedAt = DateTime.UtcNow,
                        DeviceManufacturer = DeviceInfo.Manufacturer,
                        DeviceModel = DeviceInfo.Model,
                        DeviceName = DeviceInfo.Name,
                        DeviceVersionString = DeviceInfo.VersionString,
                        DevicePlatform = DeviceInfo.Platform.ToString(),
                        DeviceIdiom = DeviceInfo.Idiom.ToString(),
                        DeviceType = DeviceInfo.DeviceType.ToString()
                    };

                    await _baseService.UserService.SaveAsync(userEntity);

                    var homeViewModel = new HomeViewModel(_baseService, SharedData, TranslationProvider);
                    var newHomePage = new AppShell(TranslationProvider)
                    {
                        BindingContext = homeViewModel
                    };

                    Application.Current.MainPage = newHomePage;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    TranslationProvider["LegalPage_ErrorTitle"],
                    TranslationProvider["LegalPage_ErrorMessage"],
                    "OK"
                );
            }
        }
    }
}