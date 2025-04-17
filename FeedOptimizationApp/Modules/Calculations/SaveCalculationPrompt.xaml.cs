using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using FeedOptimizationApp.Localization;

namespace FeedOptimizationApp.Modules.Calculations;

public partial class SaveCalculationPrompt : ContentPage
{
    private readonly TranslationProvider _translationProvider;

    public SaveCalculationPrompt(TranslationProvider translationProvider)
    {
        InitializeComponent();
        _translationProvider = translationProvider;
        BindingContext = this;

        // Listen for language changes to update translations dynamically
        _translationProvider.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == null)
            {
                OnPropertyChanged(nameof(SaveCalculationPrompt_Title));
                OnPropertyChanged(nameof(SaveCalculationPrompt_Heading));
                OnPropertyChanged(nameof(SaveCalculationPrompt_NamePlaceholder));
                OnPropertyChanged(nameof(SaveCalculationPrompt_DescriptionPlaceholder));
                OnPropertyChanged(nameof(SaveCalculationPrompt_CancelButton));
                OnPropertyChanged(nameof(SaveCalculationPrompt_SaveButton));
                OnPropertyChanged(nameof(SaveCalculationPrompt_ValidationMessage));
            }
        };
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
            await Toast.Make(SaveCalculationPrompt_ValidationMessage).Show();
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

    #region TRANSLATIONS

    public string SaveCalculationPrompt_Title => _translationProvider["SaveCalculationPrompt_Title"];
    public string SaveCalculationPrompt_Heading => _translationProvider["SaveCalculationPrompt_Heading"];
    public string SaveCalculationPrompt_NamePlaceholder => _translationProvider["SaveCalculationPrompt_NamePlaceholder"];
    public string SaveCalculationPrompt_DescriptionPlaceholder => _translationProvider["SaveCalculationPrompt_DescriptionPlaceholder"];
    public string SaveCalculationPrompt_CancelButton => _translationProvider["SaveCalculationPrompt_CancelButton"];
    public string SaveCalculationPrompt_SaveButton => _translationProvider["SaveCalculationPrompt_SaveButton"];
    public string SaveCalculationPrompt_ValidationMessage => _translationProvider["SaveCalculationPrompt_ValidationMessage"];

    #endregion TRANSLATIONS
}