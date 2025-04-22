using FeedOptimizationApp.Services.Interfaces;

namespace FeedOptimizationApp.Services
{
    /// <summary>
    /// Base service class that provides access to various application services.
    /// This class acts as a central point for accessing services used throughout the application.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Gets the user service.
        /// Provides methods for managing user-related data and operations.
        /// </summary>
        public IUserService UserService { get; }

        /// <summary>
        /// Gets the feed service.
        /// Provides methods for managing feed-related data and operations.
        /// </summary>
        public IFeedService FeedService { get; }

        /// <summary>
        /// Gets the calculation service.
        /// Provides methods for managing calculations, including feed optimization and results.
        /// </summary>
        public ICalculationService CalculationService { get; }

        /// <summary>
        /// Gets the enum entities service.
        /// Provides methods for managing enumeration entities such as species, grazing types, and body weights.
        /// </summary>
        public IEnumEntitiesService EnumEntitiesService { get; }

        /// <summary>
        /// Gets the reset picker service.
        /// Provides functionality to reset UI components like pickers (e.g., AutoCompletePicker).
        /// </summary>
        public IResetPickerService ResetPickerService { get; }

        /// <summary>
        /// Gets the translation service.
        /// Provides localized translations for UI labels and text.
        /// </summary>
        public ITranslationService TranslationService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="userService">The user service instance.</param>
        /// <param name="feedService">The feed service instance.</param>
        /// <param name="calculationService">The calculation service instance.</param>
        /// <param name="enumEntitiesService">The enum entities service instance.</param>
        /// <param name="resetPickerService">The reset picker service instance.</param>
        public BaseService(
    IUserService userService,
    IFeedService feedService,
    ICalculationService calculationService,
    IEnumEntitiesService enumEntitiesService,
    IResetPickerService resetPickerService,
    ITranslationService translationService)
        {
            UserService = userService;
            FeedService = feedService;
            CalculationService = calculationService;
            EnumEntitiesService = enumEntitiesService;
            ResetPickerService = resetPickerService;
            TranslationService = translationService;
        }
    }
}