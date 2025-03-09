using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Controls.Compatibility;

namespace FeedOptimizationApp.Helpers
{
    public class AutoCompletePicker : VerticalStackLayout
    {
        private readonly Entry _entry;
        private readonly ListView _listView;
        private ObservableCollection<object> _filteredItems = new();
        private Popup _popup;
        private CancellationTokenSource _cts = new();
        private bool _itemSelected;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(AutoCompletePicker), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AutoCompletePicker), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemDisplayBindingProperty =
            BindableProperty.Create(nameof(ItemDisplayBinding), typeof(BindingBase), typeof(AutoCompletePicker));

        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(AutoCompletePicker));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
                _entry.Text = GetDisplayText(value); // Update Entry
                HideDropdown();
            }
        }

        public BindingBase ItemDisplayBinding
        {
            get => (BindingBase)GetValue(ItemDisplayBindingProperty);
            set => SetValue(ItemDisplayBindingProperty, value);
        }

        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public AutoCompletePicker()
        {
            Spacing = 0;

            _entry = new Entry
            {
                Placeholder = "Type to search...",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            _entry.TextChanged += OnTextChanged;

            _listView = new ListView
            {
                ItemsSource = _filteredItems,
                //HeightRequest = 150,
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label();
                    label.SetBinding(Label.TextProperty, "Name");
                    return new ViewCell { View = label };
                })
            };
            _listView.ItemSelected += OnItemSelected;

            Children.Add(_entry);
        }

        private void CreatePopup()
        {
            // Get the screen width and height using DeviceDisplay
            var screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density; // Convert to pixels
            var screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density; // Convert to pixels

            // Check if the popup already exists to avoid recreating it
            if (_popup == null)
            {
                // Create the Popup with 70% width and height of the screen
                _popup = new Popup
                {
                    Content = new Border
                    {
                        Content = _listView ?? throw new ArgumentNullException(nameof(_listView), "ListView cannot be null"), // Ensure _listView is not null
                        Padding = 10, // Set padding to 10
                        BackgroundColor = Colors.White,
                        StrokeShape = new RoundRectangle
                        {
                            CornerRadius = new CornerRadius(10)
                        },
                        Stroke = new SolidColorBrush(Colors.Gray),
                        StrokeThickness = 1, // Set border thickness if needed
                        WidthRequest = screenWidth * 0.7, // Set width to 70% of screen width
                        HeightRequest = screenHeight * 0.7 // Set height to 70% of screen height
                    }
                };

                // Optionally set Popup events
                _popup.Closed += (s, e) =>
                {
                    // Check if an item was selected
                    if (!_itemSelected)
                    {
                        // Handle the case where the user canceled the selection
                        _entry.Text = string.Empty; // Clear the entry text or handle as needed
                    }

                    // Clean up resources if needed
                    _popup = null; // Clear the reference to ensure it's not reused
                };
            }
            else
            {
                // If the popup already exists, remove the _listView from its current parent
                if (_listView.Parent is Layout<View> parent)
                {
                    parent.Children.Remove(_listView);
                }

                // Reassign the _listView to the popup's content
                ((Border)_popup.Content).Content = _listView;
            }
        }

        private async void ShowPopup()
        {
            if (_filteredItems.Count > 0)
            {
                CreatePopup(); // Ensure the popup is created

                // Show the popup only if it is properly initialized
                if (_popup != null && _listView != null)
                {
                    try
                    {
                        await Application.Current.MainPage.ShowPopupAsync(_popup);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it as needed
                        Console.WriteLine($"Error showing popup: {ex.Message}");
                    }
                }
            }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is AutoCompletePicker picker && newValue is IEnumerable newItems)
            {
                picker._filteredItems.Clear();
                foreach (var item in newItems.Cast<object>())
                    picker._filteredItems.Add(item);

                picker._listView.ItemsSource = picker._filteredItems;
            }
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ItemsSource == null) return;

            try
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            catch { /* Ignore */ }

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            try
            {
                await Task.Delay(500, token); // ✅ Reduce wait time for testing
                if (token.IsCancellationRequested) return;
            }
            catch (TaskCanceledException) { return; }

            string searchText = e.NewTextValue?.ToLower() ?? string.Empty;
            _filteredItems.Clear();

            foreach (var item in ItemsSource)
            {
                string displayText = GetDisplayText(item);
                if (displayText.ToLower().Contains(searchText))
                    _filteredItems.Add(item);
            }

            Dispatcher.Dispatch(() =>
            {
                _listView.ItemsSource = null;
                _listView.ItemsSource = _filteredItems;
                _listView.IsVisible = _filteredItems.Count > 0; // ✅ Force UI update
            });

            TextChangedCommand?.Execute(e.NewTextValue);

            ShowPopup(); // Show the popup after filtering
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            _itemSelected = true; // Set the flag to true when an item is selected
            SelectedItem = e.SelectedItem;
            _entry.Text = GetDisplayText(SelectedItem);

            // Delay to ensure the selection is processed before closing
            await Task.Delay(100); // Adjust delay as needed
            HideDropdown(); // Close the popup
            _listView.SelectedItem = null; // Reset selection
            _itemSelected = false; // Reset the flag after handling the selection
        }

        private string GetDisplayText(object item)
        {
            if (item == null) return string.Empty;

            if (ItemDisplayBinding is Binding binding && binding.Path != null)
            {
                var property = item.GetType().GetProperty(binding.Path, BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                    return property.GetValue(item)?.ToString() ?? string.Empty;
            }

            // ✅ Fallback: If no binding, try to get Name explicitly
            var nameProperty = item.GetType().GetProperty("Name");
            return nameProperty?.GetValue(item)?.ToString() ?? item.ToString();
        }

        private void HideDropdown()
        {
            if (_popup != null)
            {
                _popup.Close(); // Safely close the popup if it exists
                _popup = null; // Clear the reference after closing
            }
        }

        public void Clear()
        {
            _entry.Text = string.Empty;
            SelectedItem = null;
            _filteredItems.Clear();
            _listView.ItemsSource = _filteredItems;
        }
    }
}