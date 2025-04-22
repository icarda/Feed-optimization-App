using FeedOptimizationApp.Services.Interfaces;

namespace FeedOptimizationApp.Modules.Calculations;

/// <summary>
/// Represents the page for creating a new calculation.
/// </summary>
public partial class CreateCalculationPage : ContentPage
{
    private readonly CreateCalculationViewModel _viewModel;
    private readonly IResetPickerService _pickerResetService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCalculationPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model to be used for data binding.</param>
    /// <param name="pickerResetService">The service to handle picker resets.</param>
    public CreateCalculationPage(CreateCalculationViewModel viewModel, IResetPickerService pickerResetService)
    {
        InitializeComponent();

        // Assign the provided view model to the private field and set it as the BindingContext for data binding.
        _viewModel = viewModel;
        BindingContext = _viewModel;

        // Assign the provided picker reset service to the private field.
        _pickerResetService = pickerResetService;

        // Subscribe to the reset event from the picker reset service.
        // This ensures that the ResetPicker method is called whenever the reset event is triggered.
        _pickerResetService.OnResetPicker += ResetPicker;
    }

    /// <summary>
    /// Resets the AutoCompletePicker control (FeedPicker) when the reset event is triggered.
    /// </summary>
    private void ResetPicker()
    {
        if (FeedPicker != null)
        {
            // Call the Reset method on the FeedPicker to clear its state.
            FeedPicker.Reset();
        }
        else
        {
            // Log a message if the FeedPicker is null, indicating it cannot be reset.
            Console.WriteLine("FeedPicker is null. Unable to reset.");
        }
    }

    /// <summary>
    /// Called when the page appears on the screen.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Re-subscribe to the reset event to ensure the picker can be reset when the page is active.
        _pickerResetService.OnResetPicker += ResetPicker;

        // Execute the command to load feeds into the view model.
        // This ensures that the feed data is available when the page is displayed.
        _viewModel.LoadFeedsCommand.Execute(null);
    }

    /// <summary>
    /// Called when the page is about to disappear from the screen.
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Unsubscribe from the reset event to avoid memory leaks or unintended behavior
        // when the page is no longer active.
        _pickerResetService.OnResetPicker -= ResetPicker;
    }
}