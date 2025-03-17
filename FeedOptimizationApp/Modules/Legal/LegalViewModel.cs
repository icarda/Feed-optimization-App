using DataLibrary.Models;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Modules.Home;
using FeedOptimizationApp.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Legal
{
    /// <summary>
    /// ViewModel for handling the legal terms and conditions screen.
    /// Responsible for navigating back and saving the user's acceptance of terms.
    /// </summary>
    public class LegalViewModel : BaseViewModel
    {
        // Service used to perform data operations such as saving user data.
        private readonly BaseService _baseService;

        // Service for initializing the database.
        private readonly DatabaseInitializer _databaseInitializer;

        /// <summary>
        /// Command to navigate back to the previous screen.
        /// </summary>
        public ICommand BackCommand { get; }

        /// <summary>
        /// Command to handle user agreement to the legal terms.
        /// </summary>
        public ICommand AgreeCommand { get; }

        /// <summary>
        /// Constructor that initializes the LegalViewModel with required services.
        /// </summary>
        /// <param name="baseService">Provides access to data operations.</param>
        /// <param name="sharedData">Shared data context across the application.</param>
        /// <param name="databaseInitializer">Service for initializing the database.</param>
        public LegalViewModel(BaseService baseService, SharedData sharedData, DatabaseInitializer databaseInitializer)
            : base(sharedData)
        {
            // Ensure baseService is not null; otherwise, throw an exception.
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _databaseInitializer = databaseInitializer ?? throw new ArgumentNullException(nameof(databaseInitializer));

            // Initialize commands with their respective handlers.
            BackCommand = new Command(OnBackButtonClicked);
            AgreeCommand = new Command(async () => await OnAgreeButtonClicked());
        }

        // Private backing field to hold the user's agreement status.
        private bool _hasAgreed;

        /// <summary>
        /// Indicates whether the user has agreed to the terms and conditions.
        /// </summary>
        public bool HasAgreed
        {
            get => _hasAgreed;
            set => SetProperty(ref _hasAgreed, value);
        }

        /// <summary>
        /// Handles the Back button click event.
        /// Resets the selections and clears the feed table.
        /// </summary>
        private async void OnBackButtonClicked()
        {
            // Reset the selected species, country, and language.
            SharedData.SelectedSpecies = null;
            SharedData.SelectedCountry = null;
            SharedData.SelectedLanguage = null;

            // Clear the feed table.
            await _databaseInitializer.ClearFeedsAsync();

            // Navigate back to the main page.
            Application.Current.MainPage = new MainPage(new MainViewModel(_baseService, SharedData));
        }

        /// <summary>
        /// Handles the Agree button click event.
        /// If the user has agreed to the terms, saves the user details and navigates to the home page.
        /// Otherwise, displays an alert prompting the user to agree.
        /// </summary>
        private async Task OnAgreeButtonClicked()
        {
            if (HasAgreed)
            {
                try
                {
                    // Create a new user entity with device and user details.
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

                        // Additional device details can be added here as needed.
                    };

                    // Save the user entity using the UserService.
                    await _baseService.UserService.SaveAsync(userEntity);

                    // Initialize the HomeViewModel and set it as the BindingContext for the new home page.
                    var homeViewModel = new HomeViewModel(_baseService, SharedData);
                    var newHomePage = new AppShell
                    {
                        BindingContext = homeViewModel
                    };

                    // Replace the current MainPage with the new home page.
                    Application.Current.MainPage = newHomePage;
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during the save process.
                    Debug.WriteLine($"Exception: {ex.Message}");
                }
            }
            else
            {
                // If the user has not agreed to the terms, show an alert message.
                await Application.Current.MainPage.DisplayAlert("Error", "Please agree to the terms to continue.", "OK");
            }
        }
    }
}