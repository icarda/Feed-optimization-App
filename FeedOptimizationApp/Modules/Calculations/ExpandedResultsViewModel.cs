using Android.App;
using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;

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

        private ObservableCollection<StoredFeed> _feedInfo;

        public ObservableCollection<StoredFeed> FeedInfo
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

        private List<CalculationHasFeedEntity> _feedEntitiesForResult;

        public List<CalculationHasFeedEntity> FeedEntitiesForResult
        {
            get => _feedEntitiesForResult;
            set => SetProperty(ref _feedEntitiesForResult, value);
        }

        private ObservableCollection<StoredResults> _storedResultsForDisplay;

        public ObservableCollection<StoredResults> StoredResultsForDisplay
        {
            get => _storedResultsForDisplay;
            set => SetProperty(ref _storedResultsForDisplay, value);
        }

        private string _totalRation;

        public string TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        private string _grazingName;

        public string GrazingName
        {
            get => _grazingName;
            set => SetProperty(ref _grazingName, value);
        }

        private string _bodyWeightName;

        public string BodyWeightName
        {
            get => _bodyWeightName;
            set => SetProperty(ref _bodyWeightName, value);
        }

        private string _dietQualityEstimateName;

        public string DietQualityEstimateName
        {
            get => _dietQualityEstimateName;
            set => SetProperty(ref _dietQualityEstimateName, value);
        }

        private string _nrKidsLambsName;

        public string NrKidsLambsName
        {
            get => _nrKidsLambsName;
            set => SetProperty(ref _nrKidsLambsName, value);
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
            var grazing = _baseService.EnumEntitiesService.GetGrazingByIdAsync(AnimalInfo.GrazingId).Result.Data;
            GrazingName = grazing.Name;
            var bodyWeight = _baseService.EnumEntitiesService.GetBodyWeightByIdAsync(AnimalInfo.BodyWeightId).Result.Data;
            BodyWeightName = bodyWeight.Name;
            var dietQualityEstimate = _baseService.EnumEntitiesService.GetDietQualityEstimateByIdAsync(AnimalInfo.DietQualityEstimateId).Result.Data;
            DietQualityEstimateName = dietQualityEstimate.Name;
            var nrKidsLambs = _baseService.EnumEntitiesService.GetKidsLambsByIdAsync(AnimalInfo.KidsLambsId).Result.Data;
            NrKidsLambsName = nrKidsLambs.Name;

            // Load Feed Information
            var feedEntities = await _baseService.CalculationService.GetCalculationHasFeedsByCalculationId(calculationId);
            var feedInfoList = new ObservableCollection<StoredFeed>();
            foreach (var feedEntity in feedEntities.Data)
            {
                var feed = await _baseService.FeedService.GetById(feedEntity.FeedId);
                feedInfoList.Add(new StoredFeed
                {
                    Feed = feed.Data,
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
            var results = await _baseService.CalculationService.GetCalculationHasResultByCalculationId(calculationId);
            if (results != null && results.Data != null)
            {
                var firstResult = results.Data.FirstOrDefault();
                if (firstResult != null)
                {
                    // Fetch the feed entities only once
                    var feedEntitiesForResult = await _baseService.CalculationService.GetCalculationHasFeedsByCalculationId(calculationId);
                    var storedResultsList = new ObservableCollection<StoredResults>();

                    foreach (var feedEntity in feedEntitiesForResult.Data)
                    {
                        var feed = await _baseService.FeedService.GetById(feedEntity.FeedId);

                        var resultInfo = new StoredResults
                        {
                            Feed = feed.Data,
                            CalculationId = firstResult.CalculationId,
                            GFresh = firstResult.GFresh,
                            PercentFresh = firstResult.PercentFresh,
                            PercentDryMatter = firstResult.PercentDryMatter,
                            TotalRation = firstResult.TotalRation
                        };

                        storedResultsList.Add(resultInfo);
                    }
                    StoredResultsForDisplay = storedResultsList;
                    TotalRation = storedResultsList.Sum(x => x.TotalRation).ToString("0.00");
                }
            }
        }

        // Class to represent a stored feed
        public class StoredFeed
        {
            public FeedEntity? Feed { get; set; }
            public int? CalculationId { get; set; }
            public decimal? DM { get; set; }
            public decimal? CPDM { get; set; }
            public decimal? MEMJKGDM { get; set; }
            public decimal Price { get; set; }
            public decimal Intake { get; set; }
            public decimal? MinLimit { get; set; }
            public decimal? MaxLimit { get; set; }
        }

        public class StoredResults
        {
            public FeedEntity? Feed { get; set; }
            public int? CalculationId { get; set; }
            public decimal GFresh { get; set; }
            public decimal PercentFresh { get; set; }
            public decimal PercentDryMatter { get; set; }
            public decimal TotalRation { get; set; }
        }
    }
}