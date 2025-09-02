using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Modules.Calculations;
using FeedOptimizationApp.Services;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Home;

public class HomeViewModel : BaseViewModel
{
    private readonly BaseService _baseService;

    // Command to navigate to the Create Calculation page
    public ICommand CreateCalculationCommand { get; }

    // Command to navigate to the View Calculations page
    public ICommand ViewCalculationsCommand { get; }

    public HomeViewModel(BaseService baseService, SharedData sharedData, TranslationProvider translationProvider)
        : base(sharedData, translationProvider)
    {
        _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));

        CreateCalculationCommand = new Command(async () => await OnCreateCalculationClicked());
        ViewCalculationsCommand = new Command(async () => await OnViewCalculationsClicked());
    }

    private async Task OnCreateCalculationClicked()
    {
        var viewModel = new CreateCalculationViewModel(_baseService, SharedData, TranslationProvider);
        await Shell.Current.GoToAsync("//CreateCalculationPage", new Dictionary<string, object>
        {
            { "CreateCalculationViewModel", viewModel }
        });
    }

    private async Task OnViewCalculationsClicked()
    {
        await Shell.Current.GoToAsync("//ViewCalculationsPage");
    }
}