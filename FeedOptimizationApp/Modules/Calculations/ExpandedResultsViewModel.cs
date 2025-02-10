using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Reflection;

namespace FeedOptimizationApp.Modules.Calculations
{
    public class ExpandedResultsViewModel : BaseViewModel
    {
        private readonly BaseService _baseService;

        // get the calculation ID from the shared data

        public int? CalculationId
        {
            get => SharedData.CalculationId;
            set
            {
                if (SharedData.CalculationId != value)
                {
                    SharedData.CalculationId = value;
                    OnPropertyChanged(nameof(CalculationId));
                }
            }
        }

        private CalculationEntity _animalInfo;

        public CalculationEntity AnimalInfo
        {
            get => _animalInfo;
            set => SetProperty(ref _animalInfo, value);
        }

        private ObservableCollection<CalculationHasFeedEntity> _feedInfo;

        public ObservableCollection<CalculationHasFeedEntity> FeedInfo
        {
            get => _feedInfo;
            set => SetProperty(ref _feedInfo, value);
        }

        private ObservableCollection<CalculationHasResultEntity> _calculationHasResults;

        public ObservableCollection<CalculationHasResultEntity> CalculationHasResults
        {
            get => _calculationHasResults;
            set => SetProperty(ref _calculationHasResults, value);
        }

        private string _totalRation;

        public string TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        public ExpandedResultsViewModel(BaseService baseService, SharedData sharedData)
        : base(sharedData)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            LoadResults((int)CalculationId);
        }

        private async Task LoadResults(int calculationId)
        {
            // Load Animal Information
            AnimalInfo = _baseService.CalculationService.GetCalculationById(calculationId).Result.Data;

            // Load Feed Information
            var feedEntities = await _baseService.CalculationService.GetCalculationHasFeedsByCalculationId(calculationId);
            var feedInfoList = new ObservableCollection<CalculationHasFeedEntity>();
            foreach (var feedEntity in feedEntities.Data)
            {
                //var feed = await _baseService.FeedService.GetById(feedEntity.FeedId);
                feedInfoList.Add(new CalculationHasFeedEntity
                {
                    FeedId = feedEntity.Id,
                    DM = feedEntity.DM,
                    CPDM = feedEntity.CPDM,
                    MEMJKGDM = feedEntity.MEMJKGDM,
                    Price = feedEntity.Price,
                    Intake = feedEntity.Intake,
                    MinLimit = feedEntity.MinLimit,
                    MaxLimit = feedEntity.MaxLimit
                });
            }
            FeedInfo = feedInfoList;

            // Load the results from the database using the calculationId
            var result = await _baseService.CalculationService.GetCalculationHasResultByCalculationId(calculationId);
            if (result != null && result.Data != null)
            {
                CalculationHasResults = new ObservableCollection<CalculationHasResultEntity>(result.Data);
                TotalRation = CalculationHasResults.Sum(x => x.TotalRation).ToString("0.00");
            }
        }
    }
}