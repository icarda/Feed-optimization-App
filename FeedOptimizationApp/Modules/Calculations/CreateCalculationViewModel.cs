using CommunityToolkit.Maui.Alerts;
using DataLibrary.DTOs;
using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
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

        #endregion Commands

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
                    // If the type is "Does" or "Ewes", display the sucklings field.
                    IsNrSucklingsVisible = value.Name == "Does" || value.Name == "Ewes";
                }
            }
        }

        // Selected grazing entity.
        private GrazingEntity? _selectedGrazing;

        public GrazingEntity? SelectedGrazing
        {
            get => _selectedGrazing;
            set => SetProperty(ref _selectedGrazing, value);
        }

        // Selected body weight entity.
        private BodyWeightEntity? _selectedBodyWeight;

        public BodyWeightEntity? SelectedBodyWeight
        {
            get => _selectedBodyWeight;
            set => SetProperty(ref _selectedBodyWeight, value);
        }

        // Average Daily Gain (ADG) input.
        private decimal? _ADG;

        public decimal? ADG
        {
            get => _ADG;
            set => SetProperty(ref _ADG, value);
        }

        // Selected diet quality estimate.
        private DietQualityEstimateEntity? _selectedDietQualityEstimate;

        public DietQualityEstimateEntity? SelectedDietQualityEstimate
        {
            get => _selectedDietQualityEstimate;
            set => SetProperty(ref _selectedDietQualityEstimate, value);
        }

        // Boolean indicating if the animal is in the last 8 weeks of gestation.
        private bool _isLast8WeeksOfGestation;

        public bool IsLast8WeeksOfGestation
        {
            get => _isLast8WeeksOfGestation;
            set => SetProperty(ref _isLast8WeeksOfGestation, value);
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
            set => SetProperty(ref _dailyMilkYieldValue, value);
        }

        // Milk fat content value input.
        private decimal? _fatContentValue;

        public decimal? FatContentValue
        {
            get => _fatContentValue;
            set => SetProperty(ref _fatContentValue, value);
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
        public ObservableCollection<FeedEntity> Feeds { get; set; } = new ObservableCollection<FeedEntity>();

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
                Feeds.Clear();
                var feeds = await _baseService.FeedService.GetAllAsync();
                if (feeds != null)
                {
                    foreach (var feed in feeds.Data)
                    {
                        Feeds.Add(feed);
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
                if (StoredFeeds.Count >= 3)
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
    }
}