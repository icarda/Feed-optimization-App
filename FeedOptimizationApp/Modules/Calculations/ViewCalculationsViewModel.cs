using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.ComponentModel;

namespace FeedOptimizationApp.Modules.Calculations;

public class ViewCalculationsViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly BaseService _baseService;

    //public ObservableCollection<Calculation> Calculations { get; }

    public ViewCalculationsViewModel(BaseService baseService, SharedData sharedData)
        : base(sharedData)
    {
        _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        //Calculations = new ObservableCollection<Calculation>();
        LoadCalculations();
    }

    private async void LoadCalculations()
    {
    }
}