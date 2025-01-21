namespace FeedOptimizationApp.Modules.Calculations;

public partial class ViewCalculationsPage : ContentPage
{
    private readonly ViewCalculationsViewModel _viewModel;

    public ViewCalculationsPage(ViewCalculationsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}