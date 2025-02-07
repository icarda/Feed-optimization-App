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

    public ICommand SaveCommand => new Command(async () =>
    {
        var name = NameEntry.Text;
        var description = DescriptionEntry.Text;

        // Ensure name and description are not null or empty
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
        {
            await Toast.Make("Please enter a Name and Description.").Show();
        }

        await Navigation.PopModalAsync(true);
        MessagingCenter.Send(this, "SaveCalculation", new Tuple<string, string>(name, description));
    });

    public ICommand CancelCommand => new Command(async () =>
    {
        await Navigation.PopModalAsync(true);
    });
}