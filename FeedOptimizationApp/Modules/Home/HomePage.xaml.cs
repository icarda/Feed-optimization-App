namespace FeedOptimizationApp.Modules.Home;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomePage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model to be used for data binding.</param>
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}