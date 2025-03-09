using FeedOptimizationApp.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FeedOptimizationApp.Modules;

public class BaseViewModel : INotifyPropertyChanged
{
    protected readonly SharedData SharedData;

    public BaseViewModel(SharedData sharedData)
    {
        SharedData = sharedData;
    }

    private bool isBusy = false;

    /// <summary>
    /// Gets or sets a value indicating whether the view model is busy.
    /// </summary>
    public bool IsBusy
    {
        get { return isBusy; }
        set { SetProperty(ref isBusy, value); }
    }

    private string title = string.Empty;

    /// <summary>
    /// Gets or sets the title of the view model.
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
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion INotifyPropertyChanged
}