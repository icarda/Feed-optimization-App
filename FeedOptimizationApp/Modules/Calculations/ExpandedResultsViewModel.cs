using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FeedOptimizationApp.Modules.Calculations
{
    /// <summary>
    /// ViewModel to handle the expanded results for feed optimization calculations.
    /// </summary>
    public class ExpandedResultsViewModel : BaseViewModel
    {
        // Service for accessing various data operations.
        private readonly BaseService _baseService;

        /// <summary>
        /// Gets or sets the Calculation ID using shared data storage.
        /// </summary>
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

        // Holds the animal calculation information.
        private CalculationEntity _animalInfo;

        /// <summary>
        /// Gets or sets the animal information from the calculation.
        /// </summary>
        public CalculationEntity AnimalInfo
        {
            get => _animalInfo;
            set => SetProperty(ref _animalInfo, value);
        }

        // Collection to store feed information related to the calculation.
        private ObservableCollection<StoredFeed> _feedInfo;

        /// <summary>
        /// Gets or sets the collection of feed details.
        /// </summary>
        public ObservableCollection<StoredFeed> FeedInfo
        {
            get => _feedInfo;
            set => SetProperty(ref _feedInfo, value);
        }

        // Collection to store calculation results (if multiple results exist).
        private ObservableCollection<CalculationHasResultEntity> _calculationHasResults;

        /// <summary>
        /// Gets or sets the calculation results.
        /// </summary>
        public ObservableCollection<CalculationHasResultEntity> CalculationHasResults
        {
            get => _calculationHasResults;
            set => SetProperty(ref _calculationHasResults, value);
        }

        // List to store feed entities associated with the result.
        private List<CalculationHasFeedEntity> _feedEntitiesForResult;

        /// <summary>
        /// Gets or sets the list of feed entities used for the calculation result.
        /// </summary>
        public List<CalculationHasFeedEntity> FeedEntitiesForResult
        {
            get => _feedEntitiesForResult;
            set => SetProperty(ref _feedEntitiesForResult, value);
        }

        // Collection to store the results for display purposes.
        private ObservableCollection<StoredResults> _storedResultsForDisplay;

        /// <summary>
        /// Gets or sets the stored results to be displayed in the UI.
        /// </summary>
        public ObservableCollection<StoredResults> StoredResultsForDisplay
        {
            get => _storedResultsForDisplay;
            set => SetProperty(ref _storedResultsForDisplay, value);
        }

        #region Requirements Properties

        // Properties for energy requirements
        private decimal _energyRequirementMaintenance;

        public decimal EnergyRequirementMaintenance
        {
            get => _energyRequirementMaintenance;
            set => SetProperty(ref _energyRequirementMaintenance, Math.Round(value, 2));
        }

        private decimal _energyRequirementAdditional;

        public decimal EnergyRequirementAdditional
        {
            get => _energyRequirementAdditional;
            set => SetProperty(ref _energyRequirementAdditional, Math.Round(value, 2));
        }

        private decimal _energyRequirementTotal;

        public decimal EnergyRequirementTotal
        {
            get => _energyRequirementTotal;
            set => SetProperty(ref _energyRequirementTotal, Math.Round(value, 2));
        }

        // Properties for crude protein requirements
        private decimal _crudeProteinRequirementMaintenance;

        public decimal CrudeProteinRequirementMaintenance
        {
            get => _crudeProteinRequirementMaintenance;
            set => SetProperty(ref _crudeProteinRequirementMaintenance, Math.Round(value));
        }

        private decimal _crudeProteinRequirementAdditional;

        public decimal CrudeProteinRequirementAdditional
        {
            get => _crudeProteinRequirementAdditional;
            set => SetProperty(ref _crudeProteinRequirementAdditional, Math.Round(value));
        }

        // Properties for dry matter intake estimates
        private decimal _dryMatterIntakeEstimateBase;

        public decimal DryMatterIntakeEstimateBase
        {
            get => _dryMatterIntakeEstimateBase;
            set => SetProperty(ref _dryMatterIntakeEstimateBase, Math.Round(value));
        }

        private decimal _dryMatterIntakeEstimateAdditional;

        public decimal DryMatterIntakeEstimateAdditional
        {
            get => _dryMatterIntakeEstimateAdditional;
            set => SetProperty(ref _dryMatterIntakeEstimateAdditional, Math.Round(value));
        }

        #endregion Requirements Properties

        private string _dmiRequirement;

        public string DMiRequirement
        {
            get => _dmiRequirement;
            set => SetProperty(ref _dmiRequirement, value);
        }

        private string _cpiRequirement;

        public string CPiRequirement
        {
            get => _cpiRequirement;
            set => SetProperty(ref _cpiRequirement, value);
        }

        private string _meiRequirement;

        public string MEiRequirement
        {
            get => _meiRequirement;
            set => SetProperty(ref _meiRequirement, value);
        }

        private string _totalDMi;

        public string TotalDMi
        {
            get => _totalDMi;
            set => SetProperty(ref _totalDMi, value);
        }

        private string _totalCPi;

        public string TotalCPi
        {
            get => _totalCPi;
            set => SetProperty(ref _totalCPi, value);
        }

        private string _totalMEi;

        public string TotalMEi
        {
            get => _totalMEi;
            set => SetProperty(ref _totalMEi, value);
        }

        // Total ration value to be displayed.
        private string _totalRation;

        /// <summary>
        /// Gets or sets the total ration value as a formatted string.
        /// </summary>
        public string TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        // Total ration value to be displayed.
        private string _totalFeedCost;

        /// <summary>
        /// Gets or sets the total feed cost value as a formatted string.
        /// </summary>
        public string TotalFeedCost
        {
            get => _totalFeedCost;
            set => SetProperty(ref _totalFeedCost, value);
        }

        // Display name for grazing information.
        private string _grazingName;

        /// <summary>
        /// Gets or sets the name of the grazing.
        /// </summary>
        public string GrazingName
        {
            get => _grazingName;
            set => SetProperty(ref _grazingName, value);
        }

        // Display name for body weight information.
        private string _bodyWeightName;

        /// <summary>
        /// Gets or sets the body weight name.
        /// </summary>
        public string BodyWeightName
        {
            get => _bodyWeightName;
            set => SetProperty(ref _bodyWeightName, value);
        }

        // Display name for diet quality estimate.
        private string _dietQualityEstimateName;

        /// <summary>
        /// Gets or sets the diet quality estimate name.
        /// </summary>
        public string DietQualityEstimateName
        {
            get => _dietQualityEstimateName;
            set => SetProperty(ref _dietQualityEstimateName, value);
        }

        // Display name for kids or lambs information.
        private string _nrKidsLambsName;

        /// <summary>
        /// Gets or sets the name indicating the number of kids or lambs.
        /// </summary>
        public string NrKidsLambsName
        {
            get => _nrKidsLambsName;
            set => SetProperty(ref _nrKidsLambsName, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandedResultsViewModel"/> class.
        /// Loads the results based on the provided CalculationId.
        /// </summary>
        /// <param name="baseService">Service for data operations.</param>
        /// <param name="sharedData">Shared data object that holds common properties.</param>
        public ExpandedResultsViewModel(BaseService baseService, SharedData sharedData)
            : base(sharedData)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));

            // Start loading results for the provided CalculationId
            LoadResults((int)CalculationId);
        }

        /// <summary>
        /// Asynchronously loads all necessary data related to the calculation, including animal info, feeds, and results.
        /// </summary>
        /// <param name="calculationId">The ID of the calculation to load data for.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task LoadResults(int calculationId)
        {
            try
            {
                // Load Animal Information based on calculation ID.
                var calculationResult = await _baseService.CalculationService.GetCalculationById(calculationId);
                if (calculationResult == null || calculationResult.Data == null)
                {
                    // If calculation data is not found, exit the method.
                    return;
                }
                AnimalInfo = calculationResult.Data;

                // Load Grazing Information using the GrazingId from AnimalInfo.
                var grazingResult = await _baseService.EnumEntitiesService.GetGrazingByIdAsync(AnimalInfo.GrazingId);
                GrazingName = grazingResult?.Data?.Name;

                // Load Body Weight Information using the BodyWeightId from AnimalInfo.
                var bodyWeightResult = await _baseService.EnumEntitiesService.GetBodyWeightByIdAsync(AnimalInfo.BodyWeightId);
                BodyWeightName = bodyWeightResult?.Data?.Name;

                // Load Diet Quality Estimate Information using the DietQualityEstimateId from AnimalInfo.
                var dietQualityEstimateResult = await _baseService.EnumEntitiesService.GetDietQualityEstimateByIdAsync(AnimalInfo.DietQualityEstimateId);
                DietQualityEstimateName = dietQualityEstimateResult?.Data?.Name;

                // Load Kids/Lambs Information using the KidsLambsId from AnimalInfo.
                var nrKidsLambsResult = await _baseService.EnumEntitiesService.GetKidsLambsByIdAsync(AnimalInfo.KidsLambsId);
                NrKidsLambsName = nrKidsLambsResult?.Data?.Name;

                // Load Feed Information associated with the calculation.
                var feedEntitiesResult = await _baseService.CalculationService.GetCalculationHasFeedsByCalculationId(calculationId);
                if (feedEntitiesResult == null || feedEntitiesResult.Data == null)
                {
                    // If no feed entities are found, exit the method.
                    return;
                }

                // Create a collection to hold feed information.
                var feedInfoList = new ObservableCollection<StoredFeed>();

                // Loop through each feed entity and load its corresponding feed data.
                foreach (var feedEntity in feedEntitiesResult.Data)
                {
                    var feedResult = await _baseService.FeedService.GetById(feedEntity.FeedId);
                    if (feedResult?.Data != null)
                    {
                        // Map the feed data and associated parameters to a StoredFeed object.
                        feedInfoList.Add(new StoredFeed
                        {
                            Feed = feedResult.Data,
                            DM = feedEntity.DM,
                            CPDM = feedEntity.CPDM,
                            MEMJKGDM = feedEntity.MEMJKGDM,
                            Price = feedEntity.Price,
                            Intake = feedEntity.Intake,
                            MinLimit = feedEntity.MinLimit,
                            MaxLimit = feedEntity.MaxLimit
                        });
                    }
                }
                // Update the FeedInfo property with the loaded feed information.
                FeedInfo = feedInfoList;

                // Load calculation results using the calculationId.
                var results = await _baseService.CalculationService.GetCalculationHasResultByCalculationId(calculationId);
                if (results != null && results.Data != null)
                {
                    // Use the first available result if multiple results exist.
                    var firstResult = results.Data.FirstOrDefault();
                    if (firstResult != null)
                    {
                        // Fetch feed entities again for constructing the results display.
                        var feedEntitiesForResult = await _baseService.CalculationService.GetCalculationHasFeedsByCalculationId(calculationId);
                        var storedResultsList = new ObservableCollection<StoredResults>();

                        // Loop through each feed entity to build the results information.
                        foreach (var feedEntity in feedEntitiesForResult.Data)
                        {
                            // Load the corresponding feed details.
                            var feed = await _baseService.FeedService.GetById(feedEntity.FeedId);

                            // Create a StoredResults object with the feed and result data.
                            var resultInfo = new StoredResults
                            {
                                Feed = feed.Data,
                                CalculationId = firstResult.CalculationId,
                                GFresh = feedEntity.Intake,
                                PercentFresh = Math.Round(100 * feedEntity.Intake / feedEntitiesForResult.Data.Sum(f => f.Intake), MidpointRounding.AwayFromZero),
                                PercentDryMatter = Math.Round(100 * (feedEntity.Intake * feedEntity.DM / 100) / feedEntitiesForResult.Data.Sum(f => f.Intake * f.DM / 100), MidpointRounding.AwayFromZero),
                                TotalRation = feedEntity.Price * feedEntity.Intake / 1000,
                                DMi = feedEntity.Intake * feedEntity.DM / 100,
                                CPi = (feedEntity.Intake * feedEntity.DM / 100) * feedEntity.CPDM / 100,
                                MEi = (feedEntity.Intake * feedEntity.DM / 100) * feedEntity.MEMJKGDM / 100,
                                Cost = feedEntity.Price
                            };

                            storedResultsList.Add(resultInfo);
                        }
                        // Update the display collection with the stored results.
                        StoredResultsForDisplay = storedResultsList;

                        DMiRequirement = firstResult.DMiRequirement.ToString("0.00");
                        CPiRequirement = firstResult.CPiRequirement.ToString("0.00");
                        MEiRequirement = firstResult.MEiRequirement.ToString("0.00");

                        TotalDMi = storedResultsList.Sum(x => x.DMi).ToString("0.00");
                        TotalCPi = storedResultsList.Sum(x => x.CPi).ToString("0.00");
                        TotalMEi = storedResultsList.Sum(x => x.MEi).ToString("0.00");

                        // Calculate and format the total ration value.
                        TotalRation = storedResultsList.Sum(x => x.TotalRation).ToString("0.00");

                        // Calculate and format the total feed cost.
                        TotalFeedCost = storedResultsList.Sum(x => x.Cost).ToString("0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed.
                Console.WriteLine($"An error occurred while loading results: {ex.Message}");
            }
        }
    }
}