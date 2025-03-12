using FeedOptimizationApp.Helpers;

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

        // Subscribe to the message to clear the AutoCompletePicker control
        MessagingCenter.Subscribe<CreateCalculationViewModel>(this, "ClearFeedPicker", (sender) =>
        {
            FeedPicker.Reset();
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Unsubscribe from the message when the page is disappearing
        MessagingCenter.Unsubscribe<CreateCalculationViewModel>(this, "ClearFeedPicker");
    }
}