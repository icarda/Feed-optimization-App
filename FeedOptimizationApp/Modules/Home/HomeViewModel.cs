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

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
    /// </summary>
    /// <param name="baseService">The base service to be used for data operations.</param>
    public HomeViewModel(BaseService baseService, SharedData sharedData, TranslationProvider translationProvider)
        : base(sharedData, translationProvider)
    {
        _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));

        CreateCalculationCommand = new Command(async () => await OnCreateCalculationClicked());
        ViewCalculationsCommand = new Command(async () => await OnViewCalculationsClicked());

        // Listen for language changes to update translations
        TranslationProvider.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == null)
            {
                OnPropertyChanged(nameof(PageTitle));
                OnPropertyChanged(nameof(HeadingText));
                OnPropertyChanged(nameof(SubHeadingText));
                OnPropertyChanged(nameof(CreateCalculationButtonText));
                OnPropertyChanged(nameof(ViewCalculationsButtonText));
            }
        };
    }

    // Properties for translated text
    public string PageTitle => TranslationProvider["HomePage_Title"];
    public string HeadingText => TranslationProvider["HomePage_Heading"];
    public string SubHeadingText => TranslationProvider["HomePage_SubHeading"];
    public string CreateCalculationButtonText => TranslationProvider["HomePage_CreateCalculationButton"];
    public string ViewCalculationsButtonText => TranslationProvider["HomePage_ViewCalculationsButton"];

    /// <summary>
    /// Handles the Create Calculation button click event.
    /// Navigates to the Create Calculation page.
    /// </summary>
    private async Task OnCreateCalculationClicked()
    {
        var viewModel = new CreateCalculationViewModel(_baseService, SharedData, TranslationProvider);
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