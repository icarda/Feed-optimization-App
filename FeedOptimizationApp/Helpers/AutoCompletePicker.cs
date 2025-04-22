using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FeedOptimizationApp.Localization;
using Microsoft.Maui.Controls;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Custom control for an auto-complete picker.
    /// </summary>
    public class AutoCompletePicker : StackLayout
    {
        private readonly Entry _entry;
        private readonly ListView _listView;
        private readonly ObservableCollection<object> _filteredItems;

        /// <summary>
        /// Bindable property for the items source.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(AutoCompletePicker), propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// Bindable property for the selected item.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AutoCompletePicker), defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Bindable property for the display member path.
        /// </summary>
        public static readonly BindableProperty DisplayMemberPathProperty =
            BindableProperty.Create(nameof(DisplayMemberPath), typeof(string), typeof(AutoCompletePicker));

        /// <summary>
        /// Bindable property for the text changed command.
        /// </summary>
        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(AutoCompletePicker));

        /// <summary>
        /// Bindable property for the text.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(AutoCompletePicker), string.Empty, BindingMode.TwoWay, propertyChanged: OnTextPropertyChanged);

        /// <summary>
        /// Bindable property for the placeholder.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AutoCompletePicker), string.Empty, propertyChanged: OnPlaceholderChanged);

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Gets or sets the display member path.
        /// </summary>
        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        /// <summary>
        /// Gets or sets the text changed command.
        /// </summary>
        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompletePicker"/> class.
        /// </summary>
        public AutoCompletePicker()
        {
            _filteredItems = new ObservableCollection<object>();

            _entry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            _entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
            _entry.TextChanged += OnEntryTextChanged;

            _listView = new ListView
            {
                ItemsSource = _filteredItems,
                IsVisible = false,
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label();
                    label.SetBinding(Label.TextProperty, new Binding(DisplayMemberPath));
                    return new ViewCell { View = label };
                })
            };
            _listView.ItemSelected += OnItemSelected;

            Orientation = StackOrientation.Vertical;
            Children.Add(_entry);
            Children.Add(_listView);
        }

        /// <summary>
        /// Handles changes to the items source.
        /// </summary>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompletePicker)bindable;
            control.FilterItems(control.Text);
        }

        /// <summary>
        /// Handles changes to the text property.
        /// </summary>
        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompletePicker)bindable;
            var newText = newValue?.ToString() ?? string.Empty;

            if (control._entry.Text != newText)
                control._entry.Text = newText;

            control.FilterItems(newText);
        }

        /// <summary>
        /// Handles changes to the placeholder property.
        /// </summary>
        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompletePicker)bindable;
            if (control._entry != null)
            {
                control._entry.Placeholder = newValue?.ToString();
            }
        }

        /// <summary>
        /// Handles text changes in the entry.
        /// </summary>
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text != e.NewTextValue)
                Text = e.NewTextValue;

            FilterItems(e.NewTextValue);

            if (TextChangedCommand?.CanExecute(e.NewTextValue) == true)
                TextChangedCommand.Execute(e.NewTextValue);
        }

        /// <summary>
        /// Filters the items based on the search text.
        /// </summary>
        private void FilterItems(string searchText)
        {
            if (ItemsSource == null) return;

            searchText = searchText?.ToLower() ?? string.Empty;
            _filteredItems.Clear();

            foreach (var item in ItemsSource)
            {
                string displayText = GetDisplayText(item).ToLower();
                if (displayText.Contains(searchText))
                    _filteredItems.Add(item);
            }

            _listView.IsVisible = _filteredItems.Count > 0;
        }

        /// <summary>
        /// Gets the display text for an item based on the display member path.
        /// </summary>
        private string GetDisplayText(object item)
        {
            if (item == null || string.IsNullOrEmpty(DisplayMemberPath))
                return string.Empty;

            var property = item.GetType().GetProperty(DisplayMemberPath);
            return property?.GetValue(item)?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Handles item selection in the list view.
        /// </summary>
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            SelectedItem = e.SelectedItem;
            Text = GetDisplayText(e.SelectedItem);
            _listView.IsVisible = false;
            _listView.SelectedItem = null;
        }

        /// <summary>
        /// Resets the auto-complete picker by clearing the search text and filtered items.
        /// </summary>
        public void Reset()
        {
            // Clear the search text
            Text = string.Empty;

            // Clear the selected item
            SelectedItem = null;

            // Clear the filtered items
            _filteredItems.Clear();

            // Hide the dropdown list
            _listView.IsVisible = false;
        }
    }
}
