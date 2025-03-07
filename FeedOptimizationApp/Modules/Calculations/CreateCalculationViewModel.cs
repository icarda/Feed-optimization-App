using CommunityToolkit.Maui.Alerts;
using DataLibrary.DTOs;
using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using FeedOptimizationApp.Shared;
using static FeedOptimizationApp.Modules.Calculations.ExpandedResultsViewModel;

namespace FeedOptimizationApp.Modules.Calculations
{
    /// <summary>
    /// ViewModel for creating and processing feed optimization calculations.
    /// Handles loading of feeds and animal information, validating inputs,
    /// adding feeds to the calculation, performing the calculation, and saving results.
    /// </summary>
    public class CreateCalculationViewModel : BaseViewModel
    {
        // Service used to interact with the data and perform calculations.
        private readonly BaseService _baseService;

        // Validator for the CalculationEntity inputs.
        private readonly CalculationValidator _validator;

        // Holds any validation errors encountered during input validation.
        public Dictionary<string, string> ValidationErrors { get; private set; } = new Dictionary<string, string>();

        // Shortcut to get the currently selected species from shared data.
        private SpeciesEntity? SelectedSpecies => SharedData.SelectedSpecies;

        /// <summary>
        /// Constructor for the CreateCalculationViewModel.
        /// Initializes services, validators, commands, and loads initial data.
        /// </summary>
        /// <param name="baseService">Service for data operations.</param>
        /// <param name="sharedData">Shared data object across the application.</param>
        public CreateCalculationViewModel(BaseService baseService, SharedData sharedData) : base(sharedData)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _validator = new CalculationValidator();

            // Initialize commands
            LoadFeedsCommand = new Command(async () => await LoadFeedsAsync());
            LoadAnimalInformationCommand = new Command(async () => await LoadAnimalInformationAsync());
            ClearFeedCommand = new Command(ClearFeed);
            AddFeedCommand = new Command(OnAddFeed);
            SaveResultsCommand = new Command(OnSaveResults);
            ClearAnimalInfoCommand = new Command(ClearAnimalInfo);
            ResetFeedInfoCommand = new Command(ResetFeedInfo);
            SearchCommand = new Command<string>(text => SearchText = text);

            // Execute the command to load animal information immediately
            LoadAnimalInformationCommand.Execute(null);

            // Initialize tab commands
            SetAnimalInfoActiveTab = new Command(() =>
            {
                // Activate the Animal Info tab and deactivate others
                AnimalInfoTabIsActive = true;
                FeedInfoTabIsActive = false;
                ResultsTabIsActive = false;
            });

            SetFeedInfoTabActive = new Command(() =>
            {
                // Load feeds when switching to Feed Info tab
                LoadFeedsCommand.Execute(null);

                // Save animal information inputs and retrieve calculation ID
                CalculationId = GetAnimalInformationInputs();
                // Validate the animal calculation inputs
                ValidateCalculation(Calculation);

                if (ValidationErrors.Count == 0)
                {
                    // Move to Feed Info tab if validation passes
                    AnimalInfoTabIsActive = false;
                    FeedInfoTabIsActive = true;
                    ResultsTabIsActive = false;
                }
                else
                {
                    // Display a message if there are validation errors
                    Toast.Make("Please correct the validation errors.").Show();
                }
            });

            SetResultsTabActive = new Command(async () =>
            {
                // Activate Results tab and deactivate the other tabs
                AnimalInfoTabIsActive = false;
                FeedInfoTabIsActive = false;
                ResultsTabIsActive = true;

                // Retrieve feed input IDs based on the stored feeds
                CalculationHasFeedIds = GetFeedInformationInputs((int)CalculationId);
                // Perform the calculation asynchronously
                await DoCalculationAsync((int)CalculationId, CalculationHasFeedIds);
            });

            // Initialize selected values and default states
            SelectedType = null;
            SelectedGrazing = null;
            SelectedBodyWeight = null;
            SelectedDietQualityEstimate = null;
            SelectedNumberOfSucklingKidsLambs = null;
            SelectedFeed = null;
            AnimalInfoTabIsActive = true;
        }

        #region Commands

        // Command to load the feeds from the service.
        public ICommand LoadFeedsCommand { get; private set; }

        // Command to load animal information from the service.
        public ICommand LoadAnimalInformationCommand { get; private set; }

        // Command to switch to the Animal Info tab.
        public ICommand SetAnimalInfoActiveTab { get; }

        // Command to switch to the Feed Info tab.
        public ICommand SetFeedInfoTabActive { get; }

        // Command to switch to the Results tab.
        public ICommand SetResultsTabActive { get; }

        // Command to clear the current feed input fields.
        public ICommand ClearFeedCommand { get; }

        // Command to add a feed to the stored feeds collection.
        public ICommand AddFeedCommand { get; }

        // Command to save the calculated results.
        public ICommand SaveResultsCommand { get; }

        // Command to clear the animal information fields.
        public ICommand ClearAnimalInfoCommand { get; }

        // Command to clear the stored feed information and feed information fields.
        public ICommand ResetFeedInfoCommand { get; }

        public ICommand SearchCommand { get; }

        #endregion Commands

        #region Calculation Properties

        private double _mem;

        public double MEm
        {
            get => _mem;
            set => SetProperty(ref _mem, value);
        }

        private double _memGrazing;

        public double MEmGrazing
        {
            get => _memGrazing;
            set => SetProperty(ref _memGrazing, value);
        }

        private double _meG;

        public double MEg
        {
            get => _meG;
            set => SetProperty(ref _meG, value);
        }

        private double _meGestation;

        public double MEGestation
        {
            get => _meGestation;
            set => SetProperty(ref _meGestation, value);
        }

        private double _meLactation;

        public double MELactation
        {
            get => _meLactation;
            set => SetProperty(ref _meLactation, value);
        }

        private double _energyReq;

        public double EnergyReq
        {
            get => _energyReq;
            set
            {
                if (SetProperty(ref _energyReq, value))
                {
                    CalculateDCPReqAndCPReq();
                }
            }
        }

        private double _fourPercFCM;

        public double FourPercFCM
        {
            get => _fourPercFCM;
            set => SetProperty(ref _fourPercFCM, value);
        }

        private double _dcpMaintenance;

        public double DCPMaintenance
        {
            get => _dcpMaintenance;
            set => SetProperty(ref _dcpMaintenance, value);
        }

        private double _dcpLactation;

        public double DCPLactation
        {
            get => _dcpLactation;
            set => SetProperty(ref _dcpLactation, value);
        }

        private double _cpGain;

        public double CPGain
        {
            get => _cpGain;
            set => SetProperty(ref _cpGain, value);
        }

        private double _cpLactation;

        public double CPLactation
        {
            get => _cpLactation;
            set => SetProperty(ref _cpLactation, value);
        }

        private double _dcpReq;

        public double DCPReq
        {
            get => _dcpReq;
            set => SetProperty(ref _dcpReq, value);
        }

        private double _cpReq;

        public double CPReq
        {
            get => _cpReq;
            set => SetProperty(ref _cpReq, value);
        }

        private double _dmi;

        public double DMI
        {
            get => _dmi;
            set => SetProperty(ref _dmi, value);
        }

        private double _dmiGestation;

        public double DMIGestation
        {
            get => _dmiGestation;
            set => SetProperty(ref _dmiGestation, value);
        }

        private double _dmiLactation;

        public double DMILactation
        {
            get => _dmiLactation;
            set => SetProperty(ref _dmiLactation, value);
        }

        private double _dmiReq;

        public double DMIReq
        {
            get => _dmiReq;
            set => SetProperty(ref _dmiReq, value);
        }

        #endregion Calculation Properties

        #region Calculation Property Value Updates

        private void CalculateEnergyReq()
        {
            if (SelectedBodyWeight != null && SelectedType != null)
            {
                double bodyWeight = double.Parse(SelectedBodyWeight.Name);
                double maintenanceValue = SelectedType.Name switch
                {
                    "Ewes" => Constants.ME_maintenance_EWES,
                    "Ewes and lambs" => Constants.ME_maintenance_EWES_AND_LAMBS,
                    "Weaned lambs" => Constants.ME_maintenance_WEANED_LAMBS,
                    "Rams" => Constants.ME_maintenance_RAMS,
                    _ => 0
                };

                MEm = Math.Pow(bodyWeight, 0.75) * maintenanceValue;

                // Calculate MEmGrazing
                if (SelectedGrazing != null)
                {
                    double grazingMultiplier = SelectedGrazing.Name switch
                    {
                        "None" => Constants.ME_m_GRAZING_NONE,
                        "Close by" => Constants.ME_m_GRAZING_CLOSE_BY,
                        "Open range" => Constants.ME_m_GRAZING_OPEN_RANGE,
                        "Rough mountain terrain" => Constants.ME_m_GRAZING_ROUGH_MOUNTAIN_TERRAIN,
                        _ => 0
                    };

                    MEmGrazing = MEm * grazingMultiplier;
                }

                // Calculate MEg
                if (ADG != null)
                {
                    double gainValue = SelectedType.Name switch
                    {
                        "Ewes" => Constants.ME_gain_EWES,
                        "Ewes and lambs" => Constants.ME_gain_EWES_AND_LAMBS,
                        "Weaned lambs" => Constants.ME_gain_WEANED_LAMBS,
                        "Rams" => Constants.ME_gain_RAMS,
                        _ => 0
                    };

                    MEg = (double)ADG * gainValue;
                }

                // Calculate MEGestation
                double gestationMultiplier = IsLast8WeeksOfGestation ? Constants.ME_gestation_YES : Constants.ME_gestation_NO;
                MEGestation = MEm * gestationMultiplier;

                // Calculate lactation-related properties only for EWES_AND_LAMBS
                if (SelectedType.Name == "Ewes and lambs" && DailyMilkYieldValue != null && FatContentValue != null)
                {
                    // Calculate FourPercFCM
                    FourPercFCM = (0.4 * (double)DailyMilkYieldValue) + (1.5 * (double)DailyMilkYieldValue * (double)FatContentValue * 10);

                    // Calculate MELactation
                    MELactation = FourPercFCM * Constants.ME_lactation;
                }
                else
                {
                    FourPercFCM = 0;
                    MELactation = 0;
                }

                // Calculate EnergyReq
                EnergyReq = MEm + MEmGrazing + MEg + MEGestation + MELactation;
            }
        }

        private void CalculateDCPReqAndCPReq()
        {
            if (SelectedType != null)
            {
                double maintenanceValue = SelectedType.Name switch
                {
                    "Ewes" => Constants.DCP_Maintenance_EWES,
                    "Ewes and lambs" => Constants.DCP_Maintenance_EWES_AND_LAMBS,
                    "Weaned lambs" => Constants.DCP_Maintenance_WEANED_LAMBS,
                    "Rams" => Constants.DCP_Maintenance_RAMS,
                    _ => 0
                };
                DCPMaintenance = maintenanceValue;

                // Calculate DCPLactation only for EWES_AND_LAMBS
                if (SelectedType.Name == "Ewes and lambs" && DailyMilkYieldValue != null)
                {
                    DCPLactation = EnergyReq / 1000 * (DailyMilkYieldValue <= 1.5m ? Constants.DCP_Lactation_LOW : Constants.DCP_Lactation_HIGH);
                }
                else
                {
                    DCPLactation = 0;
                }

                // Calculate CPGain
                CPGain = DCPMaintenance * 1.115 + 3.84;

                // Calculate CPLactation only for EWES_AND_LAMBS
                if (SelectedType.Name == "Ewes and lambs")
                {
                    CPLactation = DCPLactation * 1.115 * 3.84;
                }
                else
                {
                    CPLactation = 0;
                }

                // Calculate DCPReq
                DCPReq = DCPMaintenance + DCPLactation;

                // Calculate CPReq
                CPReq = CPGain + CPLactation;
            }
        }

        private void CalculateDMIReq()
        {
            if (SelectedBodyWeight != null && SelectedDietQualityEstimate != null && SelectedType != null)
            {
                double bodyWeight = double.Parse(SelectedBodyWeight.Name);
                double dietQualityEstimateValue = SelectedDietQualityEstimate.Name switch
                {
                    "Low" => SelectedType.Name switch
                    {
                        "Ewes" => Constants.DQE_EWES_LOW,
                        "Ewes and lambs" => Constants.DQE_EWES_AND_LAMBS_LOW,
                        "Weaned lambs" => Constants.DQE_WEANED_LAMBS_LOW,
                        "Rams" => Constants.DQE_RAMS_LOW,
                        _ => 0
                    },
                    "Medium" => SelectedType.Name switch
                    {
                        "Ewes" => Constants.DQE_EWES_MEDIUM,
                        "Ewes and lambs" => Constants.DQE_EWES_AND_LAMBS_MEDIUM,
                        "Weaned lambs" => Constants.DQE_WEANED_LAMBS_MEDIUM,
                        "Rams" => Constants.DQE_RAMS_MEDIUM,
                        _ => 0
                    },
                    "High" => SelectedType.Name switch
                    {
                        "Ewes" => Constants.DQE_EWES_HIGH,
                        "Ewes and lambs" => Constants.DQE_EWES_AND_LAMBS_HIGH,
                        "Weaned lambs" => Constants.DQE_WEANED_LAMBS_HIGH,
                        "Rams" => Constants.DQE_RAMS_HIGH,
                        _ => 0
                    },
                    _ => 0
                };

                DMI = bodyWeight * dietQualityEstimateValue;

                // Calculate DMIGestation
                double gestationMultiplier = IsLast8WeeksOfGestation ? Constants.DMI_gestation_YES : Constants.DMI_gestation_NO;
                DMIGestation = DMI * gestationMultiplier;

                // Calculate DMILactation only for EWES_AND_LAMBS
                if (SelectedType.Name == "Ewes and lambs" && DailyMilkYieldValue != null)
                {
                    double lactationMultiplier = DailyMilkYieldValue <= 1.5m ? Constants.DMI_lactation : Constants.DMI_lactation_HIGH;
                    DMILactation = DMI * lactationMultiplier;
                }
                else
                {
                    DMILactation = 0;
                }

                // Calculate DMIReq
                DMIReq = DMI + DMIGestation + DMILactation;
            }
        }

        #endregion Calculation Property Value Updates

        #region Properties

        // Controls the visibility of the "Results" button.
        private bool _isResultsButtonVisible = false;

        public bool IsResultsButtonVisible
        {
            get => _isResultsButtonVisible;
            set => SetProperty(ref _isResultsButtonVisible, value);
        }

        // Controls the visibility of the number of sucklings field.
        private bool _isNrSucklingsVisible;

        public bool IsNrSucklingsVisible
        {
            get => _isNrSucklingsVisible;
            set => SetProperty(ref _isNrSucklingsVisible, value);
        }

        // Text displayed on the Add Feed box.
        private string _addFeedBoxText = "Add feed";

        public string AddFeedBoxText
        {
            get => _addFeedBoxText;
            set => SetProperty(ref _addFeedBoxText, value);
        }

        // Indicates if the Add Feed section is expanded.
        private bool _isAddFeedExpanded = false;

        public bool IsAddFeedExpanded
        {
            get => _isAddFeedExpanded;
            set => SetProperty(ref _isAddFeedExpanded, value);
        }

        // Boolean properties for controlling which tab is active.
        private bool _animalInfoTabIsActive;

        public bool AnimalInfoTabIsActive
        {
            get => _animalInfoTabIsActive;
            set => SetProperty(ref _animalInfoTabIsActive, value);
        }

        private bool _resultsTabIsActive;

        public bool ResultsTabIsActive
        {
            get => _resultsTabIsActive;
            set => SetProperty(ref _resultsTabIsActive, value);
        }

        private bool _feedInfoTabIsActive;

        public bool FeedInfoTabIsActive
        {
            get => _feedInfoTabIsActive;
            set => SetProperty(ref _feedInfoTabIsActive, value);
        }

        // Indicates whether a feed has been selected.
        private bool _isFeedSelected;

        public bool IsFeedSelected
        {
            get => _isFeedSelected;
            set => SetProperty(ref _isFeedSelected, value);
        }

        // Selected animal type as a LookupDTO.
        private LookupDTO? _selectedType;

        public LookupDTO? SelectedType
        {
            get => _selectedType;
            set
            {
                if (SetProperty(ref _selectedType, value))
                {
                    if (value == null)
                    {
                        IsNrSucklingsVisible = false;
                    }
                    else
                    {
                        // Set the visibility of the number of sucklings field based on the selected type.
                        IsNrSucklingsVisible = value.Name == "Does" || value.Name == "Ewes";
                    }
                    CalculateEnergyReq();
                    CalculateDCPReqAndCPReq();
                    CalculateDMIReq();
                }
            }
        }

        // Selected grazing entity.
        private GrazingEntity? _selectedGrazing;

        public GrazingEntity? SelectedGrazing
        {
            get => _selectedGrazing;
            set
            {
                if (SetProperty(ref _selectedGrazing, value))
                {
                    CalculateEnergyReq();
                }
            }
        }

        // Selected body weight entity.
        private BodyWeightEntity? _selectedBodyWeight;

        public BodyWeightEntity? SelectedBodyWeight
        {
            get => _selectedBodyWeight;
            set
            {
                if (SetProperty(ref _selectedBodyWeight, value))
                {
                    CalculateEnergyReq();
                    CalculateDMIReq();
                }
            }
        }

        // Average Daily Gain (ADG) input.
        private decimal? _ADG = 150;

        public decimal? ADG
        {
            get => _ADG;
            set
            {
                if (SetProperty(ref _ADG, value))
                {
                    CalculateEnergyReq();
                }
            }
        }

        // Selected diet quality estimate.
        private DietQualityEstimateEntity? _selectedDietQualityEstimate;

        public DietQualityEstimateEntity? SelectedDietQualityEstimate
        {
            get => _selectedDietQualityEstimate;
            set
            {
                if (SetProperty(ref _selectedDietQualityEstimate, value))
                {
                    CalculateDMIReq();
                }
            }
        }

        // Boolean indicating if the animal is in the last 8 weeks of gestation.
        private bool _isLast8WeeksOfGestation;

        public bool IsLast8WeeksOfGestation
        {
            get => _isLast8WeeksOfGestation;
            set
            {
                if (SetProperty(ref _isLast8WeeksOfGestation, value))
                {
                    CalculateEnergyReq();
                    CalculateDMIReq();
                }
            }
        }

        // Selected number of suckling kids or lambs.
        private KidsLambsEntity? _selectedNumberOfSucklingKidsLambs;

        public KidsLambsEntity? SelectedNumberOfSucklingKidsLambs
        {
            get => _selectedNumberOfSucklingKidsLambs;
            set => SetProperty(ref _selectedNumberOfSucklingKidsLambs, value);
        }

        // Daily milk yield value input.
        private decimal? _dailyMilkYieldValue;

        public decimal? DailyMilkYieldValue
        {
            get => _dailyMilkYieldValue;
            set
            {
                if (SetProperty(ref _dailyMilkYieldValue, value))
                {
                    CalculateEnergyReq();
                    CalculateDCPReqAndCPReq();
                    CalculateDMIReq();
                }
            }
        }

        // Milk fat content value input.
        private decimal? _fatContentValue = (decimal?)6.8;

        public decimal? FatContentValue
        {
            get => _fatContentValue;
            set
            {
                if (SetProperty(ref _fatContentValue, value))
                {
                    CalculateEnergyReq();
                }
            }
        }

        // Selected feed from the list.
        private FeedEntity? _selectedFeed; // Nullable feed selection

        public FeedEntity? SelectedFeed
        {
            get => _selectedFeed;
            set
            {
                if (SetProperty(ref _selectedFeed, value))
                {
                    // Set flag to indicate whether a feed is selected.
                    IsFeedSelected = value != null;
                }
            }
        }

        // Bindable properties for CalculationHasFeed inputs.
        private decimal? _dm;

        public decimal? DM
        {
            get => _dm;
            set => SetProperty(ref _dm, value);
        }

        private decimal? _cpdm;

        public decimal? CPDM
        {
            get => _cpdm;
            set => SetProperty(ref _cpdm, value);
        }

        private decimal? _memjkgdm;

        public decimal? MEMJKGDM
        {
            get => _memjkgdm;
            set => SetProperty(ref _memjkgdm, value);
        }

        private decimal? _price;

        public decimal? Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private decimal? _intake;

        public decimal? Intake
        {
            get => _intake;
            set => SetProperty(ref _intake, value);
        }

        private decimal? _minLimit;

        public decimal? MinLimit
        {
            get => _minLimit;
            set => SetProperty(ref _minLimit, value);
        }

        private decimal? _maxLimit;

        public decimal? MaxLimit
        {
            get => _maxLimit;
            set => SetProperty(ref _maxLimit, value);
        }

        // Total ration cost or value computed from feeds.
        private decimal? _totalRation = 0;

        public decimal? TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        // A string representation for the calculation result feed (if needed).
        private string? _calculationResultFeed;

        public string? CalculationResultFeed
        {
            get => _calculationResultFeed;
            set => SetProperty(ref _calculationResultFeed, value);
        }

        // The calculation ID assigned to this calculation.
        private int? _calculationId;

        public int? CalculationId
        {
            get => _calculationId;
            set => SetProperty(ref _calculationId, value);
        }

        // The main calculation entity holding animal information.
        private CalculationEntity? _calculation;

        public CalculationEntity? Calculation
        {
            get => _calculation;
            set => SetProperty(ref _calculation, value);
        }

        // List of IDs for CalculationHasFeed records.
        private List<int>? _calculationHasFeedIds;

        public List<int>? CalculationHasFeedIds
        {
            get => _calculationHasFeedIds;
            set => SetProperty(ref _calculationHasFeedIds, value);
        }

        // List of CalculationHasFeed entities.
        private List<CalculationHasFeedEntity>? _calculationHasFeeds;

        public List<CalculationHasFeedEntity>? CalculationHasFeeds
        {
            get => _calculationHasFeeds;
            set => SetProperty(ref _calculationHasFeeds, value);
        }

        // List of IDs for CalculationHasResult records.
        private List<int>? _calculationHasResultIds;

        public List<int>? CalculationHasResultIds
        {
            get => _calculationHasResultIds;
            set => SetProperty(ref _calculationHasResultIds, value);
        }

        // List of CalculationHasResult entities.
        private List<CalculationHasResultEntity>? _calculationHasResults;

        public List<CalculationHasResultEntity>? CalculationHasResults
        {
            get => _calculationHasResults;
            set => SetProperty(ref _calculationHasResults, value);
        }

        // List of stored results used for display purposes.
        private List<StoredResults>? _storedResults;

        public List<StoredResults>? StoredResults
        {
            get => _storedResults;
            set => SetProperty(ref _storedResults, value);
        }

        #endregion Properties

        #region Collections for Options and Feeds

        // Collection of available feeds.
        private ObservableCollection<FeedEntity> _feeds = new();

        public ObservableCollection<FeedEntity> Feeds
        {
            get => _feeds;
            set => SetProperty(ref _feeds, value);
        }

        private ObservableCollection<FeedEntity> _allFeeds = new();

        public ObservableCollection<FeedEntity> AllFeeds
        {
            get => _allFeeds;
            set => SetProperty(ref _allFeeds, value);
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterFeeds();
                }
            }
        }

        // Collection of available animal types.
        public ObservableCollection<LookupDTO> Types { get; set; } = new ObservableCollection<LookupDTO>();

        // Separate collections for sheep and goat types.
        public ObservableCollection<SheepTypeEntity> SheepTypes { get; set; } = new ObservableCollection<SheepTypeEntity>();

        public ObservableCollection<GoatTypeEntity> GoatTypes { get; set; } = new ObservableCollection<GoatTypeEntity>();

        // Collection of available grazing options.
        public ObservableCollection<GrazingEntity> Grazings { get; set; } = new ObservableCollection<GrazingEntity>();

        // Collection of available body weight options.
        public ObservableCollection<BodyWeightEntity> BodyWeights { get; set; } = new ObservableCollection<BodyWeightEntity>();

        // Collection of available diet quality estimates.
        public ObservableCollection<DietQualityEstimateEntity> DietQualityEstimates { get; set; } = new ObservableCollection<DietQualityEstimateEntity>();

        // Collection of available kids/lambs options.
        public ObservableCollection<KidsLambsEntity> NrSucklingKidsLambs { get; set; } = new ObservableCollection<KidsLambsEntity>();

        #endregion Collections for Options and Feeds

        #region Methods for Data Loading and User Actions

        /// <summary>
        /// Asynchronously loads feeds from the service.
        /// </summary>
        private async Task LoadFeedsAsync()
        {
            try
            {
                AllFeeds.Clear();
                var feeds = await _baseService.FeedService.GetAllAsync();
                if (feeds != null)
                {
                    foreach (var feed in feeds.Data)
                    {
                        AllFeeds.Add(feed);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error while fetching feeds.
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Asynchronously loads animal information data such as types, grazings, body weights, kids/lambs, and diet quality estimates.
        /// </summary>
        private async Task LoadAnimalInformationAsync()
        {
            try
            {
                // Clear previous data
                Types.Clear();
                Grazings.Clear();
                BodyWeights.Clear();
                DietQualityEstimates.Clear();
                NrSucklingKidsLambs.Clear();

                // Load animal types based on the selected species.
                if (SelectedSpecies?.Name.ToLower() == "sheep")
                {
                    var types = await _baseService.EnumEntitiesService.GetSheepTypesAsync();
                    foreach (var type in types.Data)
                    {
                        Types.Add(ConversionHelpers.ConvertToLookupDTO(type));
                    }
                }
                else if (SelectedSpecies?.Name.ToLower() == "goat")
                {
                    var types = await _baseService.EnumEntitiesService.GetGoatTypesAsync();
                    foreach (var type in types.Data)
                    {
                        Types.Add(ConversionHelpers.ConvertToLookupDTO(type));
                    }
                }

                // Load available grazing options.
                var grazings = await _baseService.EnumEntitiesService.GetGrazingsAsync();
                foreach (var grazing in grazings.Data)
                {
                    Grazings.Add(grazing);
                }

                // Load available body weight options.
                var bodyWeights = await _baseService.EnumEntitiesService.GetBodyWeightsAsync();
                foreach (var bodyWeight in bodyWeights.Data)
                {
                    BodyWeights.Add(bodyWeight);
                }

                // Load kids/lambs options.
                var kidsLambs = await _baseService.EnumEntitiesService.GetKidsLambsAsync();
                foreach (var kidsLamb in kidsLambs.Data)
                {
                    NrSucklingKidsLambs.Add(kidsLamb);
                }

                // Load available diet quality estimates.
                var dietQualityEstimates = await _baseService.EnumEntitiesService.GetDietQualityEstimatesAsync();
                foreach (var dietQualityEstimate in dietQualityEstimates.Data)
                {
                    DietQualityEstimates.Add(dietQualityEstimate);
                }
            }
            catch (Exception ex)
            {
                // Log error while loading animal information.
                Console.WriteLine($"An error occurred while loading animal information: {ex.Message}");
            }
        }

        // Collection to store added feeds for the current calculation.
        public ObservableCollection<StoredFeed> StoredFeeds { get; set; } = new ObservableCollection<StoredFeed>();

        /// <summary>
        /// Adds the selected feed and its parameters to the stored feeds collection.
        /// </summary>
        private void OnAddFeed()
        {
            try
            {
                var storedFeed = new StoredFeed
                {
                    Feed = SelectedFeed,
                    CalculationId = CalculationId,
                    DM = SelectedFeed?.DryMatterPercentage,
                    CPDM = SelectedFeed?.CPPercentage,
                    MEMJKGDM = SelectedFeed?.MEMJKg,
                    Price = Price ?? 0,
                    Intake = Intake ?? 0,
                    MinLimit = MinLimit,
                    MaxLimit = MaxLimit
                };

                // Insert new feed at the beginning of the list.
                StoredFeeds.Insert(0, storedFeed);
                // Clear form inputs after adding.
                ClearAddedFeedForm();
                AddFeedBoxText = "Add additional feed";

                // If a minimum number of feeds are added, show the Results button.
                if (StoredFeeds.Count >= 1)
                {
                    IsResultsButtonVisible = true;
                    IsAddFeedExpanded = false;
                }
            }
            catch (Exception ex)
            {
                // Log any error that occurs during feed addition.
                Console.WriteLine($"An error occurred while adding the feed: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears the form inputs used for adding a new feed.
        /// </summary>
        private void ClearAddedFeedForm()
        {
            SelectedFeed = null;
            DM = null;
            CPDM = null;
            MEMJKGDM = null;
            Price = null;
            Intake = null;
            MinLimit = null;
            MaxLimit = null;
            SearchText = string.Empty; // Clear the search text
        }

        /// <summary>
        /// Clears the current feed information fields.
        /// </summary>
        private void ClearFeed()
        {
            SelectedFeed = null;
            DM = null;
            CPDM = null;
            MEMJKGDM = null;
            Price = null;
            Intake = null;
            MinLimit = null;
            MaxLimit = null;
            SearchText = string.Empty; // Clear the search text
        }

        /// <summary>
        /// Clears the animal information fields.
        /// </summary>
        private void ClearAnimalInfo()
        {
            SelectedType = null;
            SelectedGrazing = null;
            SelectedBodyWeight = null;
            ADG = 150;
            SelectedDietQualityEstimate = null;
            IsLast8WeeksOfGestation = false;
            SelectedNumberOfSucklingKidsLambs = null;
            DailyMilkYieldValue = null;
            FatContentValue = (decimal?)6.8;

            ResetFeedInfo();
        }

        /// <summary>
        /// Resets the stored feed information and feed information fields.
        /// </summary>
        private void ResetFeedInfo()
        {
            StoredFeeds.Clear();
            ClearFeed();
            AddFeedBoxText = "Add feed";
            IsResultsButtonVisible = false;
        }

        /// <summary>
        /// Gathers animal information inputs, creates a CalculationEntity, saves it,
        /// and returns the generated calculation ID.
        /// </summary>
        /// <returns>Calculation ID as an integer.</returns>
        private int GetAnimalInformationInputs()
        {
            try
            {
                var animalInformation = new CalculationEntity
                {
                    Type = SelectedType?.Name!,
                    GrazingId = SelectedGrazing?.Id ?? 0,
                    BodyWeightId = SelectedBodyWeight?.Id ?? 0,
                    ADG = ADG,
                    DietQualityEstimateId = SelectedDietQualityEstimate?.Id ?? 0,
                    Gestation = IsLast8WeeksOfGestation,
                    MilkYield = DailyMilkYieldValue,
                    FatContent = FatContentValue,
                    KidsLambsId = SelectedNumberOfSucklingKidsLambs?.Id ?? 0,
                    SpeciesId = SelectedSpecies?.Id ?? 0,
                    Name = "To be changed",
                    Description = "To be changed",
                };

                Calculation = animalInformation;

                // Save the animal information and retrieve the calculation ID.
                var calculationId = _baseService.CalculationService.SaveCalculationAsync(animalInformation).Result.Data;
                return calculationId;
            }
            catch (Exception ex)
            {
                // Log error during saving animal information.
                Console.WriteLine($"An error occurred while saving animal information: {ex.Message}");
                return 0; // Return a default value if error occurs.
            }
        }

        /// <summary>
        /// Validates the CalculationEntity using the validator.
        /// Removes the kids/lambs error if the field is not visible.
        /// </summary>
        /// <param name="calculation">The CalculationEntity to validate.</param>
        private void ValidateCalculation(CalculationEntity calculation)
        {
            var results = _validator.Validate(calculation);
            ValidationErrors.Clear();

            // If the number of sucklings is not visible, ignore its validation error.
            if (!IsNrSucklingsVisible)
            {
                results.Errors.RemoveAll(e => e.PropertyName == "KidsLambsId");
            }

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ValidationErrors[failure.PropertyName] = failure.ErrorMessage;
                }
            }

            OnPropertyChanged(nameof(ValidationErrors));
        }

        /// <summary>
        /// Gathers feed information inputs from the stored feeds, saves each feed record,
        /// and returns a list of CalculationHasFeed IDs.
        /// </summary>
        /// <param name="calculationId">The calculation ID associated with the feeds.</param>
        /// <returns>List of CalculationHasFeed IDs.</returns>
        private List<int> GetFeedInformationInputs(int calculationId)
        {
            int calcHasFeedId;
            var calcHasFeedIds = new List<int>();
            var calcFeedList = new List<CalculationHasFeedEntity>();

            foreach (var storedFeed in StoredFeeds)
            {
                try
                {
                    var calcFeed = new CalculationHasFeedEntity
                    {
                        FeedId = storedFeed.Feed!.Id,
                        CalculationId = calculationId,
                        DM = storedFeed.DM ?? 0,
                        CPDM = storedFeed.CPDM ?? 0,
                        MEMJKGDM = storedFeed.MEMJKGDM ?? 0,
                        Price = storedFeed.Price,
                        Intake = storedFeed.Intake,
                        MinLimit = storedFeed.MinLimit ?? 0,
                        MaxLimit = storedFeed.MaxLimit ?? 0
                    };

                    calcFeedList.Add(calcFeed);

                    // Save each feed record and capture its ID.
                    calcHasFeedId = _baseService.CalculationService.SaveCalculationHasFeedAsync(calcFeed).Result.Data;
                    if (calcHasFeedId != 0)
                        calcHasFeedIds.Add(calcHasFeedId);
                }
                catch (Exception ex)
                {
                    // Log error for individual feed saving.
                    Console.WriteLine($"An error occurred while saving feed input for feed ID {storedFeed.Feed?.Id}: {ex.Message}");
                }
            }
            CalculationHasFeeds = calcFeedList;

            return calcHasFeedIds;
        }

        /// <summary>
        /// Asynchronously triggers the calculation process.
        /// </summary>
        /// <param name="calculationId">The calculation ID.</param>
        /// <param name="calculationHasFeedIds">List of CalculationHasFeed IDs.</param>
        private async Task DoCalculationAsync(int calculationId, List<int> calculationHasFeedIds)
        {
            CalculateResult(calculationId, calculationHasFeedIds);
        }

        /// <summary>
        /// Performs the calculation based on the saved feed inputs.
        /// Retrieves each CalculationHasFeed record, computes cost and ration values,
        /// and updates the total ration as well as stored results.
        /// </summary>
        /// <param name="calculationId">The calculation ID.</param>
        /// <param name="calculationHasFeedIds">List of CalculationHasFeed IDs.</param>
        public async void CalculateResult(int calculationId, List<int> calculationHasFeedIds)
        {
            try
            {
                decimal totalCost = 0;
                var calcFeedInformation = new List<CalculationHasFeedEntity>();
                // Retrieve each CalculationHasFeed record using its ID.
                foreach (var calcFeedId in calculationHasFeedIds)
                {
                    try
                    {
                        var calcFeed = await _baseService.CalculationService.GetCalculationHasFeedById(calcFeedId);
                        if (calcFeed?.Data != null)
                        {
                            calcFeedInformation.Add(calcFeed.Data);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error if a specific feed record cannot be retrieved.
                        Console.WriteLine($"An error occurred while retrieving calc has feed with ID {calcFeedId}: {ex.Message}");
                    }
                }

                var calcHasResultList = new List<CalculationHasResultEntity>();
                var storedResultList = new List<StoredResults>();

                // Calculate the result for each feed.
                foreach (var info in calcFeedInformation)
                {
                    // Calculate dry matter intake, protein, energy, and cost.
                    var dmig = info.Intake * info.DM / 100;
                    var cpig = dmig * info.CPDM / 100;
                    var meimjday = dmig * info.MEMJKGDM / 1000;
                    var cost = info.Intake * info.Price / 1000;

                    totalCost += cost;

                    // Create a CalculationHasResultEntity (example values used for demonstration).
                    var calcHasResult = new CalculationHasResultEntity
                    {
                        CalculationId = calculationId,
                        GFresh = 50,
                        PercentFresh = 50,
                        PercentDryMatter = 50,
                        TotalRation = cost
                    };

                    calcHasResultList.Add(calcHasResult);

                    // Retrieve feed information for the result.
                    var feed = await _baseService.FeedService.GetById(info.FeedId);

                    // Create a stored result object for display purposes.
                    var storedResult = new StoredResults
                    {
                        Feed = feed.Data,
                        CalculationId = calculationId,
                        GFresh = 50,
                        PercentFresh = 50,
                        PercentDryMatter = 50,
                        TotalRation = cost
                    };

                    storedResultList.Add(storedResult);
                }
                // Update total ration cost.
                TotalRation = totalCost;
                CalculationHasResults = calcHasResultList;
                StoredResults = storedResultList;
            }
            catch (Exception ex)
            {
                // Log error if calculation fails.
                Console.WriteLine($"An error occurred while calculating the result: {ex.Message}");
            }
        }

        /// <summary>
        /// Initiates the process to save the calculated results.
        /// Displays a custom prompt to get name and description, then saves the calculation and its results.
        /// </summary>
        private async void OnSaveResults()
        {
            try
            {
                // Show a custom prompt page for the user to input name and description.
                var promptPage = new SaveCalculationPrompt();
                await Application.Current.MainPage.Navigation.PushModalAsync(promptPage);

                // Subscribe to the MessagingCenter event from the prompt page.
                MessagingCenter.Subscribe<SaveCalculationPrompt, Tuple<string, string>>(this, "SaveCalculation", async (sender, result) =>
                {
                    try
                    {
                        var name = result.Item1;
                        var description = result.Item2;

                        // Iterate through each CalculationHasResult and update the Calculation entity.
                        var calcHasResultIds = new List<int>();
                        foreach (var calcHasResult in CalculationHasResults)
                        {
                            var calculation = await _baseService.CalculationService.GetCalculationById(calcHasResult.CalculationId);
                            if (calculation?.Data != null)
                            {
                                calculation.Data.UpdateNameAndDescription(name, description);
                                // Update the Calculation record.
                                await _baseService.CalculationService.UpdateCalculationAsync(calculation.Data);

                                // Save the CalculationHasResult record.
                                await _baseService.CalculationService.SaveCalculationHasResultAsync(calcHasResult);
                            }
                        }

                        // Notify user of successful save.
                        await Toast.Make("Results saved successfully.").Show();

                        // Unsubscribe from the MessagingCenter event.
                        MessagingCenter.Unsubscribe<SaveCalculationPrompt, Tuple<string, string>>(this, "SaveCalculation");

                        // Show a custom alert popup indicating save completion.
                        var customAlertPopup = new CustomAlertPopup("Save complete!", "Please use the view calculations option to view your saved calculation.");
                        await Application.Current.MainPage.ShowPopupAsync(customAlertPopup);
                    }
                    catch (Exception ex)
                    {
                        // Log error if saving the calculation fails.
                        Console.WriteLine($"An error occurred while saving the calculation: {ex.Message}");
                        await Toast.Make("An error occurred while saving the calculation.").Show();
                    }
                });
            }
            catch (Exception ex)
            {
                // Log error if the prompt page fails to show.
                Console.WriteLine($"An error occurred while showing the prompt page: {ex.Message}");
                await Toast.Make("An error occurred while showing the prompt page.").Show();
            }
        }

        #endregion Methods for Data Loading and User Actions

        private void FilterFeeds()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Feeds = new ObservableCollection<FeedEntity>(_allFeeds);
            }
            else
            {
                var filtered = _allFeeds
                    .Where(f => f.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Feeds = new ObservableCollection<FeedEntity>(filtered);
            }
        }
    }
}