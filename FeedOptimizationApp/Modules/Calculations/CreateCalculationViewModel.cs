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
using System.ComponentModel;

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
            SaveOptimisationFeedCommand = new Command<StoredFeed>(OnSaveOptimisationFeed);
            EditStoredFeedCommand = new Command<StoredFeed>(OnEditStoredFeed);
            SaveStoredFeedCommand = new Command<StoredFeed>(OnSaveStoredFeed);
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
                OptimizationTabIsActive = false;
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
                    OptimizationTabIsActive = false;
                }
                else
                {
                    // Display a message if there are validation errors
                    Toast.Make("Please correct the validation errors.").Show();
                }
            });

            SetOptimizationTabActive = new Command(() =>
            {
                // Activate the Optimization tab and deactivate others
                OptimizationTabIsActive = true;
                AnimalInfoTabIsActive = false;
                FeedInfoTabIsActive = false;
                ResultsTabIsActive = false;
            });

            SetResultsTabActive = new Command(async () =>
            {
                // Activate Results tab and deactivate the other tabs
                AnimalInfoTabIsActive = false;
                FeedInfoTabIsActive = false;
                ResultsTabIsActive = true;
                OptimizationTabIsActive = false;

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

            // Observe changes to SharedData.SelectedSpecies
            SharedData.PropertyChanged += SharedData_PropertyChanged;
        }

        private void SharedData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SharedData.SelectedSpecies))
            {
                OnPropertyChanged(nameof(SelectedSpecies));
                LoadAnimalInformationCommand.Execute(null);
            }
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

        // Command to switch to the Optimization tab.

        public ICommand SetOptimizationTabActive { get; }

        // Command to switch to the Results tab.
        public ICommand SetResultsTabActive { get; }

        // Command to clear the current feed input fields.
        public ICommand ClearFeedCommand { get; }

        // Command to add a feed to the stored feeds collection.
        public ICommand AddFeedCommand { get; }

        public ICommand SaveOptimisationFeedCommand { get; }
        public ICommand EditStoredFeedCommand { get; }
        public ICommand SaveStoredFeedCommand { get; }

        // Command to save the calculated results.
        public ICommand SaveResultsCommand { get; }

        // Command to clear the animal information fields.
        public ICommand ClearAnimalInfoCommand { get; }

        // Command to clear the stored feed information and feed information fields.
        public ICommand ResetFeedInfoCommand { get; }

        public ICommand SearchCommand { get; }

        #endregion Commands

        #region Calculation Properties

        private decimal _mem;

        public decimal MEm
        {
            get => _mem;
            set => SetProperty(ref _mem, value);
        }

        private decimal _memGrazing;

        public decimal MEmGrazing
        {
            get => _memGrazing;
            set => SetProperty(ref _memGrazing, value);
        }

        private decimal _meG;

        public decimal MEg
        {
            get => _meG;
            set => SetProperty(ref _meG, value);
        }

        private decimal _meGestation;

        public decimal MEGestation
        {
            get => _meGestation;
            set => SetProperty(ref _meGestation, value);
        }

        private decimal _meLactation;

        public decimal MELactation
        {
            get => _meLactation;
            set => SetProperty(ref _meLactation, value);
        }

        private decimal _energyReq;

        public decimal EnergyReq
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

        private decimal _energyReqForUI;

        public decimal EnergyReqForUI
        {
            get => _energyReqForUI;
            set
            {
                if (SetProperty(ref _energyReqForUI, value))
                {
                }
            }
        }

        private decimal _fourPercFCM;

        public decimal FourPercFCM
        {
            get => _fourPercFCM;
            set => SetProperty(ref _fourPercFCM, value);
        }

        private decimal _dcpMaintenance;

        public decimal DCPMaintenance
        {
            get => _dcpMaintenance;
            set => SetProperty(ref _dcpMaintenance, value);
        }

        private decimal _dcpLactation;

        public decimal DCPLactation
        {
            get => _dcpLactation;
            set => SetProperty(ref _dcpLactation, value);
        }

        private decimal _cpGain;

        public decimal CPGain
        {
            get => _cpGain;
            set => SetProperty(ref _cpGain, value);
        }

        private decimal _cpLactation;

        public decimal CPLactation
        {
            get => _cpLactation;
            set => SetProperty(ref _cpLactation, value);
        }

        private decimal _dcpReq;

        public decimal DCPReq
        {
            get => _dcpReq;
            set => SetProperty(ref _dcpReq, value);
        }

        private decimal _cpReq;

        public decimal CPReq
        {
            get => _cpReq;
            set => SetProperty(ref _cpReq, value);
        }

        private decimal _dmi;

        public decimal DMI
        {
            get => _dmi;
            set => SetProperty(ref _dmi, value);
        }

        private decimal _dmiGestation;

        public decimal DMIGestation
        {
            get => _dmiGestation;
            set => SetProperty(ref _dmiGestation, value);
        }

        private decimal _dmiLactation;

        public decimal DMILactation
        {
            get => _dmiLactation;
            set => SetProperty(ref _dmiLactation, value);
        }

        private decimal _dmiReq;

        public decimal DMIReq
        {
            get => _dmiReq;
            set => SetProperty(ref _dmiReq, value);
        }

        #endregion Calculation Properties

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

        #region Calculation Property Value Updates

        private void CalculateEnergyReq()
        {
            if (SelectedBodyWeight != null && SelectedType != null)
            {
                decimal bodyWeight = decimal.Parse(SelectedBodyWeight.Name);
                decimal maintenanceValue = SelectedType.Id switch
                {
                    1 => Constants.ME_maintenance_EWES,
                    2 => Constants.ME_maintenance_EWES_AND_LAMBS,
                    3 => Constants.ME_maintenance_WEANED_LAMBS,
                    4 => Constants.ME_maintenance_RAMS,
                    _ => 0
                };

                MEm = (decimal)Math.Pow((double)bodyWeight, 0.75) * maintenanceValue;

                // Calculate MEmGrazing
                if (SelectedGrazing != null)
                {
                    decimal grazingMultiplier = SelectedGrazing.Id switch
                    {
                        1 => Constants.ME_m_GRAZING_NONE,
                        2 => Constants.ME_m_GRAZING_CLOSE_BY,
                        3 => Constants.ME_m_GRAZING_OPEN_RANGE,
                        4 => Constants.ME_m_GRAZING_ROUGH_MOUNTAIN_TERRAIN,
                        _ => 0
                    };

                    MEmGrazing = MEm * grazingMultiplier;
                }

                // Calculate MEg
                if (ADG != null)
                {
                    decimal gainValue = SelectedType.Id switch
                    {
                        1 => Constants.ME_gain_EWES,
                        2 => Constants.ME_gain_EWES_AND_LAMBS,
                        3 => Constants.ME_gain_WEANED_LAMBS,
                        4 => Constants.ME_gain_RAMS,
                        _ => 0
                    };

                    MEg = (decimal)(ADG * gainValue);
                }

                // Calculate MEGestation
                decimal gestationMultiplier = IsLast8WeeksOfGestation ? Constants.ME_gestation_YES : Constants.ME_gestation_NO;
                MEGestation = MEm * gestationMultiplier;

                // Calculate lactation-related properties only for EWES_AND_LAMBS
                if (SelectedType.Id == 2 && DailyMilkYieldValue != null && FatContentValue != null)
                {
                    // Calculate FourPercFCM
                    FourPercFCM = (decimal)((0.4m * DailyMilkYieldValue) + (1.5m * DailyMilkYieldValue * FatContentValue * 10m));

                    // Calculate MELactation
                    MELactation = FourPercFCM * Constants.ME_lactation;
                }
                else
                {
                    FourPercFCM = 0;
                    MELactation = 0;
                }

                EnergyReq = Math.Round(MEm + MEmGrazing + MEg + MEGestation + MELactation, 2);

                EnergyRequirementMaintenance = Math.Round(MEm / 1000);
                EnergyRequirementAdditional = Math.Round((EnergyReq - MEm) / 1000);
                var ermPLUSera = EnergyRequirementMaintenance + EnergyRequirementAdditional;
                EnergyRequirementTotal = Math.Round(EnergyRequirementMaintenance / ermPLUSera, 2);

                EnergyReqForUI = EnergyRequirementMaintenance + EnergyRequirementAdditional;
            }
        }

        private void CalculateDCPReqAndCPReq()
        {
            if (SelectedType != null)
            {
                decimal maintenanceValue = SelectedType.Id switch
                {
                    1 => Constants.DCP_Maintenance_EWES,
                    2 => Constants.DCP_Maintenance_EWES_AND_LAMBS,
                    3 => Constants.DCP_Maintenance_WEANED_LAMBS,
                    4 => Constants.DCP_Maintenance_RAMS,
                    _ => 0
                };
                DCPMaintenance = EnergyReq / 1000 * maintenanceValue;

                // Calculate DCPLactation only for EWES_AND_LAMBS
                if (SelectedType.Id == 2 && DailyMilkYieldValue != null)
                {
                    DCPLactation = EnergyReq / 1000 * (DailyMilkYieldValue <= 1.5m ? Constants.DCP_Lactation_LOW : Constants.DCP_Lactation_HIGH);
                }
                else
                {
                    DCPLactation = 0;
                }

                // Calculate CPGain
                CPGain = DCPMaintenance * 1.115m + 3.84m;

                // Calculate CPLactation only for EWES_AND_LAMBS
                if (SelectedType.Id == 2)
                {
                    CPLactation = (DCPLactation * 1.115m) + 3.84m;
                }
                else
                {
                    CPLactation = 0;
                }

                DCPReq = Math.Round(DCPMaintenance + DCPLactation);
                CPReq = Math.Round(CPGain + CPLactation);

                CrudeProteinRequirementMaintenance = Math.Round(1.115m * DCPMaintenance + 3.84m);
                CrudeProteinRequirementAdditional = Math.Round(1.115m * (DCPReq - DCPMaintenance) + 3.84m);
            }
        }

        private void CalculateDMIReq()
        {
            if (SelectedBodyWeight != null && SelectedDietQualityEstimate != null && SelectedType != null)
            {
                decimal bodyWeight = decimal.Parse(SelectedBodyWeight.Name);
                decimal dietQualityEstimateValue = SelectedDietQualityEstimate.Id switch
                {
                    1 => SelectedType.Id switch
                    {
                        1 => Constants.DQE_EWES_LOW,
                        2 => Constants.DQE_EWES_AND_LAMBS_LOW,
                        3 => Constants.DQE_WEANED_LAMBS_LOW,
                        4 => Constants.DQE_RAMS_LOW,
                        _ => 0
                    },
                    2 => SelectedType.Id switch
                    {
                        1 => Constants.DQE_EWES_MEDIUM,
                        2 => Constants.DQE_EWES_AND_LAMBS_MEDIUM,
                        3 => Constants.DQE_WEANED_LAMBS_MEDIUM,
                        4 => Constants.DQE_RAMS_MEDIUM,
                        _ => 0
                    },
                    3 => SelectedType.Id switch
                    {
                        1 => Constants.DQE_EWES_HIGH,
                        2 => Constants.DQE_EWES_AND_LAMBS_HIGH,
                        3 => Constants.DQE_WEANED_LAMBS_HIGH,
                        4 => Constants.DQE_RAMS_HIGH,
                        _ => 0
                    },
                    _ => 0
                };

                DMI = (decimal)Math.Pow((double)bodyWeight, 0.75) * dietQualityEstimateValue;

                // Calculate DMIGestation
                decimal gestationMultiplier = IsLast8WeeksOfGestation ? Constants.DMI_gestation_YES : Constants.DMI_gestation_NO;
                DMIGestation = DMI * gestationMultiplier;

                // Calculate DMILactation only for EWES_AND_LAMBS
                if (SelectedType.Id == 2 && DailyMilkYieldValue != null)
                {
                    decimal lactationMultiplier = DailyMilkYieldValue <= 1.5m ? Constants.DMI_lactation : Constants.DMI_lactation_HIGH;
                    DMILactation = DMI * lactationMultiplier;
                }
                else
                {
                    DMILactation = 0;
                }

                DMIReq = Math.Round(DMI + DMIGestation + DMILactation);
                DryMatterIntakeEstimateBase = DMI;
                DryMatterIntakeEstimateAdditional = Math.Round(DMIReq - DMI);
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

        private bool _isStoredFeedEditable = false;

        public bool IsStoredFeedEditable
        {
            get => _isStoredFeedEditable;
            set => SetProperty(ref _isStoredFeedEditable, value);
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

        private bool _optimizationTabIsActive;

        public bool OptimizationTabIsActive
        {
            get => _optimizationTabIsActive;
            set => SetProperty(ref _optimizationTabIsActive, value);
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
        private decimal? _fatContentValue = (decimal?)0.068;

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

        private decimal? _balanceDMi = 0;

        public decimal? BalanceDMi
        {
            get => _balanceDMi;
            set => SetProperty(ref _balanceDMi, value.HasValue ? Math.Round(value.Value) : value);
        }

        private decimal? _balanceCPi = 0;

        public decimal? BalanceCPi
        {
            get => _balanceCPi;
            set => SetProperty(ref _balanceCPi, value.HasValue ? Math.Round(value.Value) : value);
        }

        private decimal? _balanceMEi = 0;

        public decimal? BalanceMEi
        {
            get => _balanceMEi;
            set => SetProperty(ref _balanceMEi, value.HasValue ? Math.Round(value.Value) : value);
        }

        private decimal? _totalDMi = 0;

        public decimal? TotalDMi
        {
            get => _totalDMi;
            set => SetProperty(ref _totalDMi, value.HasValue ? Math.Round(value.Value) : value);
        }

        private decimal? _totalCPi = 0;

        public decimal? TotalCPi
        {
            get => _totalCPi;
            set => SetProperty(ref _totalCPi, value.HasValue ? Math.Round(value.Value) : value);
        }

        private decimal? _totalMEi = 0;

        public decimal? TotalMEi
        {
            get => _totalMEi;
            set => SetProperty(ref _totalMEi, value.HasValue ? Math.Round(value.Value) : value);
        }

        // Total ration cost or value computed from feeds.
        private decimal? _totalRation = 0;

        public decimal? TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        // Total feed cost or value computed from feeds.
        private decimal? _totalFeedCost = 0;

        public decimal? TotalFeedCost
        {
            get => _totalFeedCost;
            set => SetProperty(ref _totalFeedCost, value);
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
        private ObservableCollection<StoredFeed> _storedFeeds = new();

        public ObservableCollection<StoredFeed> StoredFeeds
        {
            get => _storedFeeds;
            set => SetProperty(ref _storedFeeds, value);
        }

        /// <summary>
        /// Adds the selected feed and its parameters to the stored feeds collection.
        /// </summary>
        private void OnAddFeed()
        {
            try
            {
                var dmig = (SelectedFeed?.DryMatterPercentage ?? 0) * (Intake ?? 0) / 100;
                var cpig = dmig * (SelectedFeed?.CPPercentage ?? 0) / 100;
                var meimjday = dmig * (SelectedFeed?.MEMJKg ?? 0) / 1000;
                var cost = (Intake ?? 0) * (Price ?? 0) / 1000;

                TotalDMi += Math.Round(dmig);
                TotalCPi += Math.Round(cpig);
                TotalMEi += Math.Round(meimjday);
                TotalRation += cost;
                TotalFeedCost += Price;

                BalanceDMi = Math.Round((TotalDMi ?? 0) - DMIReq);
                BalanceCPi = Math.Round((TotalCPi ?? 0) - CPReq);
                BalanceMEi = Math.Round((TotalMEi ?? 0) - EnergyReqForUI);

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
                    MaxLimit = MaxLimit,
                    DMi = Math.Round(dmig),
                    CPi = Math.Round(cpig),
                    MEi = Math.Round(meimjday),
                    Cost = Math.Round(cost)
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

                // Send a message to clear the AutoCompletePicker control
                //MessagingCenter.Send(this, "ClearFeedPicker");
            }
            catch (Exception ex)
            {
                // Log any error that occurs during feed addition.
                Console.WriteLine($"An error occurred while adding the feed: {ex.Message}");
            }
        }

        /*private void OnSaveOptimisationFeed(StoredFeed feed)
        {
            // Find the feed in the collection
            var existingFeed = StoredFeeds.FirstOrDefault(f => f.Feed.Id == feed.Feed.Id);
            if (existingFeed != null)
            {
                var index = StoredFeeds.IndexOf(feed);
                var index2 = StoredFeeds.IndexOf(existingFeed);
                // Update Intake based on DMi
                feed.Intake = Math.Round((feed.DMi / feed.DM ?? 0) * 100);

                // Remove and re-add to force UI update
                StoredFeeds.RemoveAt(index);
                StoredFeeds.Insert(index, feed);
                StoredFeeds.RemoveAt(index2);
                StoredFeeds.Insert(index2, existingFeed);
                // Recalculate totals and balances
                RecalculateTotalsAndBalances();

                // Notify UI of changes
                OnPropertyChanged(nameof(StoredFeeds));
            }
        }*/

        private void OnSaveOptimisationFeed(StoredFeed feed)
        {
            var existingFeed = StoredFeeds.FirstOrDefault(f => f.Feed.Id == feed.Feed.Id);
            if (existingFeed != null)
            {
                // Update Intake based on DMi
                existingFeed.Intake = Math.Round((feed.DMi / feed.DM ?? 0) * 100);

                // Notify UI of changes
                OnPropertyChanged(nameof(existingFeed.Intake));
                OnPropertyChanged(nameof(StoredFeeds)); // Optional but ensures full update

                // Recalculate totals and balances
                RecalculateTotalsAndBalances();
            }
        }

        /// <summary>
        /// Sets the flag to indicate that the stored feed is editable and notifies the UI to update.
        /// </summary>
        private void OnEditStoredFeed(StoredFeed feed)
        {
            IsStoredFeedEditable = true;
        }

        /// <summary>
        /// Updates the selected feed with new values, recalculates totals and balances, and notifies the UI to update.
        /// </summary>
        /// <param name="feed">The feed to be saved.</param>

        private void OnSaveStoredFeed(StoredFeed feed)
        {
            var existingFeed = StoredFeeds.FirstOrDefault(f => f.Feed.Id == feed.Feed.Id);
            if (existingFeed != null)
            {
                // Update DMi based on Intake
                existingFeed.DMi = Math.Round((feed.DM ?? 0) * (feed.Intake) / 100);

                // Notify UI of changes
                OnPropertyChanged(nameof(existingFeed.DMi));
                OnPropertyChanged(nameof(StoredFeeds));

                // Recalculate totals and balances
                RecalculateTotalsAndBalances();

                IsStoredFeedEditable = false;
            }
        }

        /// <summary>
        /// Recalculates the total DMi, CPi, MEi, and cost for all stored feeds, and updates the balances.
        /// </summary>
        private void RecalculateTotalsAndBalances()
        {
            TotalDMi = StoredFeeds.Sum(f => f.DMi);
            TotalCPi = StoredFeeds.Sum(f => f.CPi);
            TotalMEi = StoredFeeds.Sum(f => f.MEi);
            TotalRation = Math.Round(StoredFeeds.Sum(f => f.Cost), 2);

            BalanceDMi = Math.Round((TotalDMi ?? 0) - DMIReq);
            BalanceCPi = Math.Round((TotalCPi ?? 0) - CPReq);
            BalanceMEi = Math.Round((TotalMEi ?? 0) - EnergyReqForUI);
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
            //SearchText = string.Empty; // Clear the search text
            MessagingCenter.Send(this, "ClearFeedPicker");
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
            //SearchText = string.Empty; // Clear the search text
            MessagingCenter.Send(this, "ClearFeedPicker");
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
            FatContentValue = (decimal?)0.068;

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

                decimal sumOfFeedIntakes = 0;
                decimal sumOfDMig = 0;

                foreach (var info in calcFeedInformation)
                {
                    sumOfFeedIntakes += info.Intake;

                    sumOfDMig += info.Intake * info.DM / 100;
                }

                // Calculate the result for each feed.
                foreach (var info in calcFeedInformation)
                {
                    // Calculate dry matter intake, protein, energy, and cost.
                    var dmig = info.Intake * info.DM / 100;
                    var cpig = dmig * info.CPDM / 100;
                    var meimjday = dmig * info.MEMJKGDM / 1000;
                    var cost = info.Intake * info.Price / 1000;

                    totalCost += cost;

                    // Create a CalculationHasResultEntity
                    var calcHasResult = new CalculationHasResultEntity
                    {
                        CalculationId = calculationId,
                        GFresh = Math.Round(info.Intake),
                        PercentFresh = Math.Round(100 * info.Intake / sumOfFeedIntakes, MidpointRounding.AwayFromZero),
                        PercentDryMatter = Math.Round(100 * dmig / sumOfDMig, MidpointRounding.AwayFromZero),
                        TotalRation = cost,
                        DMi = Math.Round(dmig),
                        CPi = Math.Round(cpig),
                        MEi = Math.Round(meimjday),
                        Cost = Math.Round(cost),
                        DMiRequirement = DMIReq,
                        CPiRequirement = CPReq,
                        MEiRequirement = EnergyReqForUI
                    };

                    calcHasResultList.Add(calcHasResult);

                    // Retrieve feed information for the result.
                    var feed = await _baseService.FeedService.GetById(info.FeedId);

                    // Create a stored result object for display purposes.
                    var storedResult = new StoredResults
                    {
                        Feed = feed.Data,
                        CalculationId = calculationId,
                        GFresh = Math.Round(info.Intake),
                        PercentFresh = Math.Round(100 * info.Intake / sumOfFeedIntakes, MidpointRounding.AwayFromZero),
                        PercentDryMatter = Math.Round(100 * dmig / sumOfDMig, MidpointRounding.AwayFromZero),
                        TotalRation = cost,
                        DMi = Math.Round(dmig),
                        CPi = Math.Round(cpig),
                        MEi = Math.Round(meimjday),
                        Cost = Math.Round(cost),
                        DMiRequirement = DMIReq,
                        CPiRequirement = CPReq,
                        MEiRequirement = EnergyReqForUI
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

                        // Send a message to clear the AutoCompletePicker control
                        //MessagingCenter.Send(this, "ClearFeedPicker");

                        // Clear the form and stored feeds after saving.
                        ClearAnimalInfo();
                        AnimalInfoTabIsActive = true;
                        FeedInfoTabIsActive = false;
                        ResultsTabIsActive = false;
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