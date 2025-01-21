namespace FeedOptimizationApp.Modules.Calculations;

public partial class CreateCalculationPage : ContentPage
{
    private readonly CreateCalculationViewModel _viewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCalculationPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model to be used for data binding.</param>
    public CreateCalculationPage(CreateCalculationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}