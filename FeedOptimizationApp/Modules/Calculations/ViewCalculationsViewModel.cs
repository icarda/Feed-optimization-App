using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Calculations
{
    public class ViewCalculationsViewModel : BaseViewModel
    {
        private readonly BaseService _baseService;

        public List<CalculationHasResultEntity> CalculationHasResults { get; set; } = new();

        private ObservableCollection<CalculationHasResultEntity> _orderedCalculationHasResults;

        public ObservableCollection<CalculationHasResultEntity> OrderedCalculationHasResults
        {
            get => _orderedCalculationHasResults;
            set => SetProperty(ref _orderedCalculationHasResults, value);
        }

        private ObservableCollection<CalculationDisplayModel> _calculationsDisplayList;

        public ObservableCollection<CalculationDisplayModel> CalculationsDisplayList
        {
            get => _calculationsDisplayList;
            set => SetProperty(ref _calculationsDisplayList, value);
        }

        public ICommand ExpandViewCommand { get; }

        public ViewCalculationsViewModel(BaseService baseService, SharedData sharedData, TranslationProvider translationProvider)
    : base(sharedData, translationProvider)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            ExpandViewCommand = new Command(OnExpandView);
            LoadCalculations();

            // Listen for language changes to update translations
            TranslationProvider.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == null)
                {
                    OnPropertyChanged(nameof(ViewCalculationsPage_Title));
                    OnPropertyChanged(nameof(ViewCalculationsPage_HeaderTitle));
                    OnPropertyChanged(nameof(ViewCalculationsPage_HeaderDate));
                    OnPropertyChanged(nameof(ViewCalculationsPage_HeaderNoFeeds));
                    OnPropertyChanged(nameof(ViewCalculationsPage_HeaderType));
                    OnPropertyChanged(nameof(ViewCalculationsPage_HeaderAction));
                    OnPropertyChanged(nameof(ViewCalculationsPage_ExpandView));
                }
            };
        }

        private async void OnExpandView(object parameter)
        {
            if (parameter is int calculationId)
            {
                SharedData.CalculationId = calculationId;
                var viewModel = new ExpandedResultsViewModel(_baseService, SharedData, TranslationProvider);
                await Application.Current.MainPage.Navigation.PushAsync(new ExpandedResultsViewPage(viewModel));
            }
        }

        public async void LoadCalculations()
        {
            try
            {
                var result = await _baseService.CalculationService.GetAllCalculationHasResults();
                CalculationHasResults = result.Data;

                var groupedResults = CalculationHasResults.GroupBy(x => x.CalculationId);

                var displayList = new ObservableCollection<CalculationDisplayModel>();

                foreach (var group in groupedResults)
                {
                    var calculationId = group.Key;
                    var calculation = await _baseService.CalculationService.GetCalculationById(calculationId);
                    var calculationName = calculation.Data.Name;
                    var calculationType = calculation.Data.Type;
                    var calculationDate = calculation.Data.CreatedDate;
                    var numberOfFeeds = _baseService.CalculationService.GetNumberOfFeedsInCalculationHasFeedByCalculationId(calculationId).Result.Data;

                    displayList.Add(new CalculationDisplayModel
                    {
                        CalculationId = calculationId,
                        CalculationTitle = calculationName,
                        CalculationDate = calculationDate.ToString("yyyy-MM-dd"),
                        CalculationNrOfFeeds = numberOfFeeds.ToString(),
                        CalculationSpeciesType = calculationType
                    });
                }

                CalculationsDisplayList = displayList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading calculations: {ex.Message}");
            }
        }

        #region TRANSLATIONS

        public string ViewCalculationsPage_Title => TranslationProvider["ViewCalculationsPage_Title"];
        public string ViewCalculationsPage_HeaderTitle => TranslationProvider["ViewCalculationsPage_HeaderTitle"];
        public string ViewCalculationsPage_HeaderDate => TranslationProvider["ViewCalculationsPage_HeaderDate"];
        public string ViewCalculationsPage_HeaderNoFeeds => TranslationProvider["ViewCalculationsPage_HeaderNoFeeds"];
        public string ViewCalculationsPage_HeaderType => TranslationProvider["ViewCalculationsPage_HeaderType"];
        public string ViewCalculationsPage_HeaderAction => TranslationProvider["ViewCalculationsPage_HeaderAction"];
        public string ViewCalculationsPage_ExpandView => TranslationProvider["ViewCalculationsPage_ExpandView"];


        #endregion TRANSLATIONS

        public class CalculationDisplayModel
        {
            public int CalculationId { get; set; }
            public string CalculationTitle { get; set; }
            public string CalculationDate { get; set; }
            public string CalculationNrOfFeeds { get; set; }
            public string CalculationSpeciesType { get; set; }
        }
    }
}