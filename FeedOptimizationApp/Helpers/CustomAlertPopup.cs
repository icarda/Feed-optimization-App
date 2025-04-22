using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Custom alert popup for displaying messages.
    /// </summary>
    public class CustomAlertPopup : Popup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAlertPopup"/> class with a single "OK" button.
        /// </summary>
        /// <param name="title">The title of the popup.</param>
        /// <param name="message">The message to display in the popup.</param>
        /// <param name="okButtonText">The text to display on the OK button.</param>
        public CustomAlertPopup(string title, string message, string okButtonText)
        {
            // Create a grid layout for the popup content.
            var grid = new Grid
            {
                Padding = new Thickness(20), // Add padding around the grid.
                BackgroundColor = Colors.White, // Set the background color of the grid.
                VerticalOptions = LayoutOptions.Center, // Center the grid vertically.
                HorizontalOptions = LayoutOptions.Center, // Center the grid horizontally.
                WidthRequest = 300, // Set the width of the grid.
                HeightRequest = 200, // Set the height of the grid.
                RowDefinitions = // Define the rows for the grid.
                {
                    new RowDefinition { Height = GridLength.Auto }, // Row for the title.
                    new RowDefinition { Height = GridLength.Star }, // Row for the message.
                    new RowDefinition { Height = GridLength.Auto }  // Row for the button.
                }
            };

            // Create a label for the title.
            var titleLabel = new Label
            {
                Text = title, // Set the title text.
                FontAttributes = FontAttributes.Bold, // Make the title bold.
                HorizontalOptions = LayoutOptions.Center // Center the title horizontally.
            };

            // Create a label for the message.
            var messageLabel = new Label
            {
                Text = message, // Set the message text.
                HorizontalOptions = LayoutOptions.Center, // Center the message horizontally.
                VerticalOptions = LayoutOptions.Center // Center the message vertically.
            };

            // Create an OK button to close the popup.
            var okButton = new Button
            {
                Text = okButtonText, // Set the button text.
                Command = new Command(() => Close()) // Close the popup when the button is clicked.
            };

            // Add the title, message, and button to the grid.
            grid.Add(titleLabel, 0, 0); // Add the title to the first row.
            grid.Add(messageLabel, 0, 1); // Add the message to the second row.
            grid.Add(okButton, 0, 2); // Add the button to the third row.

            // Set the content of the popup to a bordered grid.
            Content = new Border
            {
                Content = grid, // Set the grid as the content.
                StrokeShape = new RoundRectangle { CornerRadius = 10 }, // Add rounded corners.
                Stroke = Colors.Gray, // Set the border color.
                StrokeThickness = 2, // Set the border thickness.
                BackgroundColor = Colors.White, // Set the background color.
                VerticalOptions = LayoutOptions.Center, // Center the border vertically.
                HorizontalOptions = LayoutOptions.Center, // Center the border horizontally.
                Margin = new Thickness(20) // Add margin around the border.
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAlertPopup"/> class with "Confirm" and "Cancel" buttons.
        /// </summary>
        /// <param name="title">The title of the popup.</param>
        /// <param name="message">The message to display in the popup.</param>
        /// <param name="confirmButtonText">The text to display on the Confirm button.</param>
        /// <param name="cancelButtonText">The text to display on the Cancel button.</param>
        /// <param name="onConfirm">The action to perform when the Confirm button is clicked.</param>
        public CustomAlertPopup(string title, string message, string confirmButtonText, string cancelButtonText, Action onConfirm)
        {
            // Create a grid layout for the popup content.
            var grid = new Grid
            {
                Padding = new Thickness(20), // Add padding around the grid.
                BackgroundColor = Colors.White, // Set the background color of the grid.
                VerticalOptions = LayoutOptions.Center, // Center the grid vertically.
                HorizontalOptions = LayoutOptions.Center, // Center the grid horizontally.
                WidthRequest = 300, // Set the width of the grid.
                HeightRequest = 200, // Set the height of the grid.
                RowDefinitions = // Define the rows for the grid.
                {
                    new RowDefinition { Height = GridLength.Auto }, // Row for the title.
                    new RowDefinition { Height = GridLength.Star }, // Row for the message.
                    new RowDefinition { Height = GridLength.Auto }  // Row for the buttons.
                }
            };

            // Create a label for the title.
            var titleLabel = new Label
            {
                Text = title, // Set the title text.
                FontAttributes = FontAttributes.Bold, // Make the title bold.
                HorizontalOptions = LayoutOptions.Center // Center the title horizontally.
            };

            // Create a label for the message.
            var messageLabel = new Label
            {
                Text = message, // Set the message text.
                HorizontalOptions = LayoutOptions.Center, // Center the message horizontally.
                VerticalOptions = LayoutOptions.Center // Center the message vertically.
            };

            // Create a Confirm button to close the popup and invoke the confirmation action.
            var confirmButton = new Button
            {
                Text = confirmButtonText, // Set the button text.
                Command = new Command(() =>
                {
                    Close(); // Close the popup.
                    onConfirm?.Invoke(); // Invoke the confirmation action if provided.
                })
            };

            // Create a Cancel button to close the popup.
            var cancelButton = new Button
            {
                Text = cancelButtonText, // Set the button text.
                Command = new Command(() => Close()) // Close the popup when the button is clicked.
            };

            // Create a stack layout for the buttons.
            var buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal, // Arrange buttons horizontally.
                HorizontalOptions = LayoutOptions.Center, // Center the buttons horizontally.
                Spacing = 10, // Add spacing between buttons.
                Children = { confirmButton, cancelButton } // Add the Confirm and Cancel buttons.
            };

            // Add the title, message, and button stack to the grid.
            grid.Add(titleLabel, 0, 0); // Add the title to the first row.
            grid.Add(messageLabel, 0, 1); // Add the message to the second row.
            grid.Add(buttonStack, 0, 2); // Add the button stack to the third row.

            // Set the content of the popup to a bordered grid.
            Content = new Border
            {
                Content = grid, // Set the grid as the content.
                StrokeShape = new RoundRectangle { CornerRadius = 10 }, // Add rounded corners.
                Stroke = Colors.Gray, // Set the border color.
                StrokeThickness = 2, // Set the border thickness.
                BackgroundColor = Colors.White, // Set the background color.
                VerticalOptions = LayoutOptions.Center, // Center the border vertically.
                HorizontalOptions = LayoutOptions.Center, // Center the border horizontally.
                Margin = new Thickness(20) // Add margin around the border.
            };
        }
    }
}
