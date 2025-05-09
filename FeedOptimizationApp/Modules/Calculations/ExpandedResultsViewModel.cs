using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;

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

        private decimal _dmiRequirement;

        public decimal DMiRequirement
        {
            get => _dmiRequirement;
            set => SetProperty(ref _dmiRequirement, value);
        }

        private decimal _cpiRequirement;

        public decimal CPiRequirement
        {
            get => _cpiRequirement;
            set => SetProperty(ref _cpiRequirement, value);
        }

        private decimal _meiRequirement;

        public decimal MEiRequirement
        {
            get => _meiRequirement;
            set => SetProperty(ref _meiRequirement, value);
        }

        private decimal _totalDMi;

        public decimal TotalDMi
        {
            get => _totalDMi;
            set => SetProperty(ref _totalDMi, value);
        }

        private decimal _totalCPi;

        public decimal TotalCPi
        {
            get => _totalCPi;
            set => SetProperty(ref _totalCPi, value);
        }

        private decimal _totalMEi;

        public decimal TotalMEi
        {
            get => _totalMEi;
            set => SetProperty(ref _totalMEi, value);
        }

        private decimal _balanceDMi;

        public decimal BalanceDMi
        {
            get => _balanceDMi;
            set => SetProperty(ref _balanceDMi, value);
        }

        private decimal _balanceCPi;

        public decimal BalanceCPi
        {
            get => _balanceCPi;
            set => SetProperty(ref _balanceCPi, value);
        }

        private decimal _balanceMEi;

        public decimal BalanceMEi
        {
            get => _balanceMEi;
            set => SetProperty(ref _balanceMEi, value);
        }

        // Total ration value to be displayed.
        private decimal _totalRation;

        /// <summary>
        /// Gets or sets the total ration value as a formatted string.
        /// </summary>
        public decimal TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        // Total ration value to be displayed.
        private decimal _totalFeedCost;

        /// <summary>
        /// Gets or sets the total feed cost value as a formatted string.
        /// </summary>
        public decimal TotalFeedCost
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
        public ExpandedResultsViewModel(BaseService baseService, SharedData sharedData, TranslationProvider translationProvider)
            : base(sharedData, translationProvider)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));

            // Start loading results for the provided CalculationId
            LoadResults((int)CalculationId);

            // Listen for language changes to update translations
            TranslationProvider.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == null)
                {
                    OnPropertyChanged(nameof(ExpandedResultsPage_Title));
                    OnPropertyChanged(nameof(ExpandedResultsPage_AnimalData));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Requirements));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Type));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Grazing));
                    OnPropertyChanged(nameof(ExpandedResultsPage_BodyWeight));
                    OnPropertyChanged(nameof(ExpandedResultsPage_ADG));
                    OnPropertyChanged(nameof(ExpandedResultsPage_DietQualityEstimate));
                    OnPropertyChanged(nameof(ExpandedResultsPage_LastGestation));
                    OnPropertyChanged(nameof(ExpandedResultsPage_NoSucklingKidsLambs));
                    OnPropertyChanged(nameof(ExpandedResultsPage_DailyMilkYield));
                    OnPropertyChanged(nameof(ExpandedResultsPage_FatContent));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Energy));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Maintenance));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Additional));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Total));
                    OnPropertyChanged(nameof(ExpandedResultsPage_CrudeProtein));
                    OnPropertyChanged(nameof(ExpandedResultsPage_DMI));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Base));
                    OnPropertyChanged(nameof(ExpandedResultsPage_NutrientRequirements));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Feed));
                    OnPropertyChanged(nameof(ExpandedResultsPage_DMi));
                    OnPropertyChanged(nameof(ExpandedResultsPage_CPi));
                    OnPropertyChanged(nameof(ExpandedResultsPage_MEi));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Cost));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Requirement));
                    OnPropertyChanged(nameof(ExpandedResultsPage_Balance));
                    OnPropertyChanged(nameof(ExpandedResultsPage_TotalFeedCostLabel));
                    OnPropertyChanged(nameof(ExpandedResultsPage_TotalRationCostLabel));
                    OnPropertyChanged(nameof(ExpandedResultsPage_CostUnitLabel));
                    OnPropertyChanged(nameof(ExpandedResultsPage_RationUnitLabel));
                }
            };
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
                                GFresh = Math.Round(feedEntity.Intake, MidpointRounding.AwayFromZero),
                                PercentFresh = Math.Round(100 * feedEntity.Intake / feedEntitiesForResult.Data.Sum(f => f.Intake), MidpointRounding.AwayFromZero),
                                PercentDryMatter = Math.Round(100 * (feedEntity.Intake * feedEntity.DM / 100) / feedEntitiesForResult.Data.Sum(f => f.Intake * f.DM / 100), MidpointRounding.AwayFromZero),
                                TotalRation = feedEntity.Price * feedEntity.Intake / 1000,
                                DMi = Math.Round(feedEntity.Intake * feedEntity.DM / 100, MidpointRounding.AwayFromZero),
                                CPi = Math.Round((feedEntity.Intake * feedEntity.DM / 100) * feedEntity.CPDM / 100, MidpointRounding.AwayFromZero),
                                MEi = Math.Round((feedEntity.Intake * feedEntity.DM / 100) * feedEntity.MEMJKGDM / 100, MidpointRounding.AwayFromZero),
                                Cost = Math.Round(feedEntity.Price, MidpointRounding.AwayFromZero)
                            };

                            storedResultsList.Add(resultInfo);
                        }
                        // Update the display collection with the stored results.
                        StoredResultsForDisplay = storedResultsList;

                        EnergyRequirementMaintenance = Math.Round(firstResult.EnergyRequirementMaintenance, MidpointRounding.AwayFromZero);
                        EnergyRequirementAdditional = Math.Round(firstResult.EnergyRequirementAdditional, MidpointRounding.AwayFromZero);
                        EnergyRequirementTotal = firstResult.EnergyRequirementTotal;
                        CrudeProteinRequirementMaintenance = Math.Round(firstResult.CrudeProteinRequirementMaintenance, MidpointRounding.AwayFromZero);
                        CrudeProteinRequirementAdditional = Math.Round(firstResult.CrudeProteinRequirementAdditional, MidpointRounding.AwayFromZero);
                        DryMatterIntakeEstimateBase = Math.Round(firstResult.DryMatterIntakeEstimateBase, MidpointRounding.AwayFromZero);
                        DryMatterIntakeEstimateAdditional = Math.Round(firstResult.DryMatterIntakeEstimateAdditional, MidpointRounding.AwayFromZero);

                        TotalDMi = Math.Round(storedResultsList.Sum(x => x.DMi), MidpointRounding.AwayFromZero);
                        TotalCPi = Math.Round(storedResultsList.Sum(x => x.CPi), MidpointRounding.AwayFromZero);
                        TotalMEi = Math.Round(storedResultsList.Sum(x => x.MEi), MidpointRounding.AwayFromZero);

                        DMiRequirement = Math.Round(firstResult.DMiRequirement, MidpointRounding.AwayFromZero);
                        CPiRequirement = Math.Round(firstResult.CPiRequirement, MidpointRounding.AwayFromZero);
                        MEiRequirement = Math.Round(firstResult.MEiRequirement, MidpointRounding.AwayFromZero);

                        BalanceDMi = TotalDMi - DMiRequirement;
                        BalanceCPi = TotalCPi - CPiRequirement;
                        BalanceMEi = TotalMEi - MEiRequirement;

                        // Calculate and format the total ration value.
                        TotalRation = Math.Round(storedResultsList.Sum(x => x.TotalRation), 2);

                        // Calculate and format the total feed cost.
                        TotalFeedCost = Math.Round(storedResultsList.Sum(x => x.Cost), MidpointRounding.AwayFromZero);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed.
                Console.WriteLine($"An error occurred while loading results: {ex.Message}");
            }
        }

        #region TRANSLATIONS

        public string ExpandedResultsPage_Title => TranslationProvider["ExpandedResultsPage_Title"];
        public string ExpandedResultsPage_AnimalData => TranslationProvider["ExpandedResultsPage_AnimalData"];
        public string ExpandedResultsPage_Requirements => TranslationProvider["ExpandedResultsPage_Requirements"];
        public string ExpandedResultsPage_Type => TranslationProvider["ExpandedResultsPage_Type"];
        public string ExpandedResultsPage_Grazing => TranslationProvider["ExpandedResultsPage_Grazing"];
        public string ExpandedResultsPage_BodyWeight => TranslationProvider["ExpandedResultsPage_BodyWeight"];
        public string ExpandedResultsPage_ADG => TranslationProvider["ExpandedResultsPage_ADG"];
        public string ExpandedResultsPage_DietQualityEstimate => TranslationProvider["ExpandedResultsPage_DietQualityEstimate"];
        public string ExpandedResultsPage_LastGestation => TranslationProvider["ExpandedResultsPage_LastGestation"];
        public string ExpandedResultsPage_NoSucklingKidsLambs => TranslationProvider["ExpandedResultsPage_NoSucklingKidsLambs"];
        public string ExpandedResultsPage_DailyMilkYield => TranslationProvider["ExpandedResultsPage_DailyMilkYield"];
        public string ExpandedResultsPage_FatContent => TranslationProvider["ExpandedResultsPage_FatContent"];
        public string ExpandedResultsPage_Energy => TranslationProvider["ExpandedResultsPage_Energy"];
        public string ExpandedResultsPage_Maintenance => TranslationProvider["ExpandedResultsPage_Maintenance"];
        public string ExpandedResultsPage_Additional => TranslationProvider["ExpandedResultsPage_Additional"];
        public string ExpandedResultsPage_Total => TranslationProvider["ExpandedResultsPage_Total"];
        public string ExpandedResultsPage_CrudeProtein => TranslationProvider["ExpandedResultsPage_CrudeProtein"];
        public string ExpandedResultsPage_DMI => TranslationProvider["ExpandedResultsPage_DMI"];
        public string ExpandedResultsPage_Base => TranslationProvider["ExpandedResultsPage_Base"];
        public string ExpandedResultsPage_NutrientRequirements => TranslationProvider["ExpandedResultsPage_NutrientRequirements"];
        public string ExpandedResultsPage_Feed => TranslationProvider["ExpandedResultsPage_Feed"];
        public string ExpandedResultsPage_DMi => TranslationProvider["ExpandedResultsPage_DMi"];
        public string ExpandedResultsPage_CPi => TranslationProvider["ExpandedResultsPage_CPi"];
        public string ExpandedResultsPage_MEi => TranslationProvider["ExpandedResultsPage_MEi"];
        public string ExpandedResultsPage_Cost => TranslationProvider["ExpandedResultsPage_Cost"];
        public string ExpandedResultsPage_Requirement => TranslationProvider["ExpandedResultsPage_Requirement"];
        public string ExpandedResultsPage_Balance => TranslationProvider["ExpandedResultsPage_Balance"];
        public string ExpandedResultsPage_TotalFeedCostLabel => TranslationProvider["ExpandedResultsPage_TotalFeedCostLabel"];
        public string ExpandedResultsPage_TotalRationCostLabel => TranslationProvider["ExpandedResultsPage_TotalRationCostLabel"];
        public string ExpandedResultsPage_CostUnitLabel => TranslationProvider["ExpandedResultsPage_CostUnitLabel"];
        public string ExpandedResultsPage_RationUnitLabel => TranslationProvider["ExpandedResultsPage_RationUnitLabel"];


        #endregion TRANSLATIONS
    }
}