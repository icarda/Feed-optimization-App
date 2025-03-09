using FeedOptimizationApp.Helpers;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
    /// </summary>
    /// <param name="baseService">The base service to be used for data operations.</param>
    public HomeViewModel(BaseService baseService, SharedData sharedData)
        : base(sharedData)
    {
        _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));

        CreateCalculationCommand = new Command(async () => await OnCreateCalculationClicked());
        ViewCalculationsCommand = new Command(async () => await OnViewCalculationsClicked());
    }

    /// <summary>
    /// Handles the Create Calculation button click event.
    /// Navigates to the Create Calculation page.
    /// </summary>
    private async Task OnCreateCalculationClicked()
    {
        var viewModel = new CreateCalculationViewModel(_baseService, SharedData);
        await Shell.Current.GoToAsync("//CreateCalculationPage", new Dictionary<string, object>
        {
            { "CreateCalculationViewModel", viewModel }
        });
    }

    /// <summary>
    /// Handles the View Calculations button click event.
    /// Navigates to the View Calculations page.
    /// </summary>
    private async Task OnViewCalculationsClicked()
    {
        await Shell.Current.GoToAsync("//ViewCalculationsPage");
    }
}