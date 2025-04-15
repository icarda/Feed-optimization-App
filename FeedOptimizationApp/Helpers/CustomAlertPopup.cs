using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Custom alert popup for displaying messages.
    /// </summary>
    public class CustomAlertPopup : Popup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAlertPopup"/> class.
        /// </summary>
        /// <param name="title">The title of the popup.</param>
        /// <param name="message">The message to display in the popup.</param>
        public CustomAlertPopup(string title, string message)
        {
            // Create a grid layout for the popup content.
            var grid = new Grid
            {
                Padding = new Thickness(20),
                BackgroundColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 300,
                HeightRequest = 200,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            // Create a label for the title.
            var titleLabel = new Label
            {
                Text = title,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a label for the message.
            var messageLabel = new Label
            {
                Text = message,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Create an OK button to close the popup.
            var okButton = new Button
            {
                Text = "OK",
                Command = new Command(() => Close())
            };

            // Add the title, message, and button to the grid.
            grid.Add(titleLabel, 0, 0);
            grid.Add(messageLabel, 0, 1);
            grid.Add(okButton, 0, 2);

            // Set the content of the popup to a bordered grid.
            Content = new Border
            {
                Content = grid,
                StrokeShape = new RoundRectangle { CornerRadius = 10 },
                Stroke = Colors.Gray,
                StrokeThickness = 2,
                BackgroundColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(20)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAlertPopup"/> class.
        /// </summary>
        /// <param name="title">The title of the popup.</param>
        /// <param name="message">The message to display in the popup.</param>
        /// <param name="onConfirm">The action to perform when the user confirms.</param>
        public CustomAlertPopup(string title, string message, Action onConfirm)
        {
            // Create a grid layout for the popup content.
            var grid = new Grid
            {
                Padding = new Thickness(20),
                BackgroundColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 300,
                HeightRequest = 200,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            // Create a label for the title.
            var titleLabel = new Label
            {
                Text = title,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a label for the message.
            var messageLabel = new Label
            {
                Text = message,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Create a Confirm button to close the popup and invoke the confirmation action.
            var okButton = new Button
            {
                Text = "Confirm",
                Command = new Command(() =>
                {
                    Close();
                    onConfirm?.Invoke();
                })
            };

            // Create a Cancel button to close the popup.
            var cancelButton = new Button
            {
                Text = "Cancel",
                Command = new Command(() => Close())
            };

            // Create a stack layout for the buttons.
            var buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 10,
                Children = { okButton, cancelButton }
            };

            // Add the title, message, and button stack to the grid.
            grid.Add(titleLabel, 0, 0);
            grid.Add(messageLabel, 0, 1);
            grid.Add(buttonStack, 0, 2);

            // Set the content of the popup to a bordered grid.
            Content = new Border
            {
                Content = grid,
                StrokeShape = new RoundRectangle { CornerRadius = 10 },
                Stroke = Colors.Gray,
                StrokeThickness = 2,
                BackgroundColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(20)
            };
        }
    }
}