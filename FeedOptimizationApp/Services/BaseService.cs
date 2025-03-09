using FeedOptimizationApp.Services.Interfaces;

namespace FeedOptimizationApp.Services
{
    /// <summary>
    /// Base service class that provides access to various services.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Gets the user service.
        /// </summary>
        public IUserService UserService { get; }

        /// <summary>
        /// Gets the feed service.
        /// </summary>
        public IFeedService FeedService { get; }

        /// <summary>
        /// Gets the calculation service.
        /// </summary>
        public ICalculationService CalculationService { get; }

        /// <summary>
        /// Gets the enum entities service.
        /// </summary>
        public IEnumEntitiesService EnumEntitiesService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="feedService">The feed service.</param>
        /// <param name="calculationService">The calculation service.</param>
        /// <param name="enumEntitiesService">The enum entities service.</param>
        public BaseService(IUserService userService, IFeedService feedService, ICalculationService calculationService, IEnumEntitiesService enumEntitiesService)
        {
            UserService = userService;
            FeedService = feedService;
            CalculationService = calculationService;
            EnumEntitiesService = enumEntitiesService;
        }
    }
}