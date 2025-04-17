using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FeedOptimizationApp.Modules
{
    /// <summary>
    /// Base class for all view models in the application.
    /// Provides common functionality such as property change notification and shared data access.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Shared data object that holds application-wide state and data.
        /// </summary>
        protected readonly SharedData SharedData;

        protected readonly TranslationProvider TranslationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="sharedData">The shared data object to be used by the view model.</param>
        public BaseViewModel(SharedData sharedData, TranslationProvider translationProvider)
        {
            SharedData = sharedData;
            TranslationProvider = translationProvider;
        }

        private bool isBusy = false;

        /// <summary>
        /// Gets or sets a value indicating whether the view model is busy.
        /// This property is typically used to show or hide loading indicators in the UI.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string title = string.Empty;

        /// <summary>
        /// Gets or sets the title of the view model.
        /// This property is typically used to set the title of a page or section in the UI.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /// <summary>
        /// Sets the property and notifies listeners of the change.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingStore">The backing field for the property.</param>
        /// <param name="value">The new value for the property.</param>
        /// <param name="propertyName">The name of the property. This is optional and will be automatically provided by the compiler.</param>
        /// <param name="onChanged">An optional action to be invoked after the property is changed.</param>
        /// <returns>True if the value was changed, false if the new value is equal to the old value.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            // Check if the new value is equal to the current value.
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            // Update the backing field with the new value.
            backingStore = value;

            // Invoke the optional action if provided.
            onChanged?.Invoke();

            // Notify listeners that the property value has changed.
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. This is optional and will be automatically provided by the compiler.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Raise the PropertyChanged event with the specified property name.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}