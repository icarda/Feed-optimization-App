using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Calculations;

public partial class SaveCalculationPrompt : ContentPage
{
    public SaveCalculationPrompt()
    {
        InitializeComponent();
        BindingContext = this;
    }

    /// <summary>
    /// Command to save the calculation with the provided name and description.
    /// </summary>
    public ICommand SaveCommand => new Command(async () =>
    {
        var name = NameEntry.Text;
        var description = DescriptionEntry.Text;

        // Ensure name and description are not null or empty
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            await Toast.Make("Please enter a Name and Description.").Show();
            return; // Exit the command if validation fails
        }

        // Close the modal and send the name and description using MessagingCenter
        await Navigation.PopModalAsync(true);
        MessagingCenter.Send(this, "SaveCalculation", new Tuple<string, string>(name, description));
    });

    /// <summary>
    /// Command to cancel the save operation and close the modal.
    /// </summary>
    public ICommand CancelCommand => new Command(async () =>
    {
        await Navigation.PopModalAsync(true);
    });
}