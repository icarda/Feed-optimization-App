namespace FeedOptimizationApp.Modules.Legal;

public partial class LegalPage : ContentPage
{
    private readonly LegalViewModel _viewModel;

    public LegalPage(LegalViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}