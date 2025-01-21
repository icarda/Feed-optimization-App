using FeedOptimizationApp.Services.Interfaces;

namespace FeedOptimizationApp.Services;

public class BaseService
{
    public IUserService UserService { get; }
    public IFeedService FeedService { get; }
    public ICalculationService CalculationService { get; }

    public BaseService(IUserService userService, IFeedService feedService, ICalculationService calculationService)
    {
        UserService = userService;
        FeedService = feedService;
        CalculationService = calculationService;
    }
}