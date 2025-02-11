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

            var titleLabel = new Label
            {
                Text = title,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            var messageLabel = new Label
            {
                Text = message,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var okButton = new Button
            {
                Text = "OK",
                Command = new Command(() => Close())
            };

            grid.Add(titleLabel, 0, 0);
            grid.Add(messageLabel, 0, 1);
            grid.Add(okButton, 0, 2);

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