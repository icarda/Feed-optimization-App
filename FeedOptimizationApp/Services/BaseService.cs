using FeedOptimizationApp.Services.Interfaces;

namespace FeedOptimizationApp.Services;

public class BaseService
{
    public IUserService UserService { get; }
    public IFeedService FeedService { get; }
    public ICalculationService CalculationService { get; }
    public IEnumEntitiesService EnumEntitiesService { get; }

    public BaseService(IUserService userService, IFeedService feedService, ICalculationService calculationService, IEnumEntitiesService enumEntitiesService)
    {
        UserService = userService;
        FeedService = feedService;
        CalculationService = calculationService;
        EnumEntitiesService = enumEntitiesService;
    }
}