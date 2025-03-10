using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Helpers
{
    public class AutoCompletePicker : VerticalStackLayout
    {
        private readonly Entry _entry;
        private readonly ListView _listView;
        private ObservableCollection<object> _filteredItems = new();
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
                IsVisible = false, // Initially hidden
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label();
                    label.SetBinding(Label.TextProperty, "Name");
                    return new ViewCell { View = label };
                })
            };
            _listView.ItemSelected += OnItemSelected;

            Children.Add(_entry);
            Children.Add(_listView); // Add ListView below Entry
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
                await Task.Delay(500, token); // Reduce wait time for testing
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
                _listView.IsVisible = _filteredItems.Count > 0; // Show ListView if there are filtered items
            });

            TextChangedCommand?.Execute(e.NewTextValue);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            _itemSelected = true; // Set the flag to true when an item is selected
            SelectedItem = e.SelectedItem;
            _entry.Text = GetDisplayText(SelectedItem);

            HideDropdown(); // Hide the dropdown
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

            // Fallback: If no binding, try to get Name explicitly
            var nameProperty = item.GetType().GetProperty("Name");
            return nameProperty?.GetValue(item)?.ToString() ?? item.ToString();
        }

        private void HideDropdown()
        {
            _listView.IsVisible = false; // Hide the ListView
        }

        public void Clear()
        {
            _entry.Text = string.Empty;
            SelectedItem = null;
            _filteredItems.Clear();
            _listView.ItemsSource = _filteredItems;
            HideDropdown(); // Ensure the dropdown is hidden
        }
    }
}