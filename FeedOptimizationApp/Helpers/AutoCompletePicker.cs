using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace FeedOptimizationApp.Helpers
{
    public class AutoCompletePicker : StackLayout
    {
        private readonly Entry _entry;
        private readonly ListView _listView;
        private readonly ObservableCollection<object> _filteredItems;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(AutoCompletePicker), propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AutoCompletePicker), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty DisplayMemberPathProperty =
            BindableProperty.Create(nameof(DisplayMemberPath), typeof(string), typeof(AutoCompletePicker));

        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(AutoCompletePicker));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(AutoCompletePicker), string.Empty, BindingMode.TwoWay, propertyChanged: OnTextPropertyChanged);

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public AutoCompletePicker()
        {
            _filteredItems = new ObservableCollection<object>();

            _entry = new Entry
            {
                Placeholder = "Type to search...",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
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

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompletePicker)bindable;
            control.FilterItems(control.Text);
        }

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompletePicker)bindable;
            var newText = newValue?.ToString() ?? string.Empty;

            if (control._entry.Text != newText)
                control._entry.Text = newText;

            control.FilterItems(newText);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text != e.NewTextValue)
                Text = e.NewTextValue;

            FilterItems(e.NewTextValue);

            if (TextChangedCommand?.CanExecute(e.NewTextValue) == true)
                TextChangedCommand.Execute(e.NewTextValue);
        }

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

        private string GetDisplayText(object item)
        {
            if (item == null || string.IsNullOrEmpty(DisplayMemberPath))
                return string.Empty;

            var property = item.GetType().GetProperty(DisplayMemberPath);
            return property?.GetValue(item)?.ToString() ?? string.Empty;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            SelectedItem = e.SelectedItem;
            Text = GetDisplayText(e.SelectedItem);
            _listView.IsVisible = false;
            _listView.SelectedItem = null;
        }

        public void Reset()
        {
            // Clear the search text
            Text = string.Empty;

            // Clear the filtered items
            _filteredItems.Clear();

            // Hide the dropdown list
            _listView.IsVisible = false;
        }
    }
}