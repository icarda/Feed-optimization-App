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
    public class CreateCalculationViewModel : BaseViewModel
    {
        private readonly BaseService _baseService;
        private readonly CalculationValidator _validator;
        public Dictionary<string, string> ValidationErrors { get; private set; } = new Dictionary<string, string>();

        private SpeciesEntity? SelectedSpecies => SharedData.SelectedSpecies;

        // Constructor
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

            // Execute the commands to load data
            LoadAnimalInformationCommand.Execute(null);

            // Initialize tab commands
            SetAnimalInfoActiveTab = new Command(() =>
            {
                AnimalInfoTabIsActive = true;
                FeedInfoTabIsActive = false;
                ResultsTabIsActive = false;
            });

            SetFeedInfoTabActive = new Command(() =>
            {
                LoadFeedsCommand.Execute(null);

                CalculationId = GetAnimalInformationInputs();
                ValidateCalculation(Calculation);

                if (ValidationErrors.Count == 0)
                {
                    AnimalInfoTabIsActive = false;
                    FeedInfoTabIsActive = true;
                    ResultsTabIsActive = false;
                }
                else
                {
                    // Optionally, display a message to the user
                    Toast.Make("Please correct the validation errors.").Show();
                }
            });

            SetResultsTabActive = new Command(async () =>
            {
                AnimalInfoTabIsActive = false;
                FeedInfoTabIsActive = false;
                ResultsTabIsActive = true;

                // Perform calculation
                CalculationHasFeedIds = GetFeedInformationInputs((int)CalculationId);
                await DoCalculationAsync((int)CalculationId, CalculationHasFeedIds);
            });

            // Initialize properties
            SelectedType = null;
            SelectedGrazing = null;
            SelectedBodyWeight = null;
            SelectedDietQualityEstimate = null;
            SelectedNumberOfSucklingKidsLambs = null;
            SelectedFeed = null;
            AnimalInfoTabIsActive = true;
        }

        // Commands for loading data and setting active tabs
        public ICommand LoadFeedsCommand { get; private set; }

        public ICommand LoadAnimalInformationCommand { get; private set; }
        public ICommand SetAnimalInfoActiveTab { get; }
        public ICommand SetFeedInfoTabActive { get; }
        public ICommand SetResultsTabActive { get; }
        public ICommand ClearFeedCommand { get; }
        public ICommand AddFeedCommand { get; }
        public ICommand SaveResultsCommand { get; }

        #region Properties

        // Properties for controlling UI elements
        private bool _isResultsButtonVisible = false;

        public bool IsResultsButtonVisible
        {
            get => _isResultsButtonVisible;
            set => SetProperty(ref _isResultsButtonVisible, value);
        }

        private bool _isNrSucklingsVisible;

        public bool IsNrSucklingsVisible
        {
            get => _isNrSucklingsVisible;
            set => SetProperty(ref _isNrSucklingsVisible, value);
        }

        private string _addFeedBoxText = "Add feed";

        public string AddFeedBoxText
        {
            get => _addFeedBoxText;
            set => SetProperty(ref _addFeedBoxText, value);
        }

        private bool _isAddFeedExpanded = false;

        public bool IsAddFeedExpanded
        {
            get => _isAddFeedExpanded;
            set => SetProperty(ref _isAddFeedExpanded, value);
        }

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

        private bool _isFeedSelected;

        public bool IsFeedSelected
        {
            get => _isFeedSelected;
            set => SetProperty(ref _isFeedSelected, value);
        }

        // Properties for holding selected values
        private LookupDTO? _selectedType;

        public LookupDTO? SelectedType
        {
            get => _selectedType;
            set
            {
                if (SetProperty(ref _selectedType, value))
                {
                    IsNrSucklingsVisible = value.Name == "Does" || value.Name == "Ewes";
                }
            }
        }

        private GrazingEntity? _selectedGrazing;

        public GrazingEntity? SelectedGrazing
        {
            get => _selectedGrazing;
            set => SetProperty(ref _selectedGrazing, value);
        }

        private BodyWeightEntity? _selectedBodyWeight;

        public BodyWeightEntity? SelectedBodyWeight
        {
            get => _selectedBodyWeight;
            set => SetProperty(ref _selectedBodyWeight, value);
        }

        private decimal? _ADG;

        public decimal? ADG
        {
            get => _ADG;
            set => SetProperty(ref _ADG, value);
        }

        private DietQualityEstimateEntity? _selectedDietQualityEstimate;

        public DietQualityEstimateEntity? SelectedDietQualityEstimate
        {
            get => _selectedDietQualityEstimate;
            set => SetProperty(ref _selectedDietQualityEstimate, value);
        }

        private bool _isLast8WeeksOfGestation;

        public bool IsLast8WeeksOfGestation
        {
            get => _isLast8WeeksOfGestation;
            set => SetProperty(ref _isLast8WeeksOfGestation, value);
        }

        private KidsLambsEntity? _selectedNumberOfSucklingKidsLambs;

        public KidsLambsEntity? SelectedNumberOfSucklingKidsLambs
        {
            get => _selectedNumberOfSucklingKidsLambs;
            set => SetProperty(ref _selectedNumberOfSucklingKidsLambs, value);
        }

        private decimal? _dailyMilkYieldValue;

        public decimal? DailyMilkYieldValue
        {
            get => _dailyMilkYieldValue;
            set => SetProperty(ref _dailyMilkYieldValue, value);
        }

        private decimal? _fatContentValue;

        public decimal? FatContentValue
        {
            get => _fatContentValue;
            set => SetProperty(ref _fatContentValue, value);
        }

        private FeedEntity? _selectedFeed; // Declare as nullable

        public FeedEntity? SelectedFeed
        {
            get => _selectedFeed;
            set
            {
                if (SetProperty(ref _selectedFeed, value))
                {
                    IsFeedSelected = value != null;
                }
            }
        }

        // Bindable properties for CalculationHasFeed fields
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

        private decimal? _totalRation = 0;

        public decimal? TotalRation
        {
            get => _totalRation;
            set => SetProperty(ref _totalRation, value);
        }

        private string? _calculationResultFeed;

        public string? CalculationResultFeed
        {
            get => _calculationResultFeed;
            set => SetProperty(ref _calculationResultFeed, value);
        }

        private int? _calculationId;

        public int? CalculationId
        {
            get => _calculationId;
            set => SetProperty(ref _calculationId, value);
        }

        private CalculationEntity? _calculation;

        public CalculationEntity? Calculation
        {
            get => _calculation;
            set => SetProperty(ref _calculation, value);
        }

        private List<int>? _calculationHasFeedIds;

        public List<int>? CalculationHasFeedIds
        {
            get => _calculationHasFeedIds;
            set => SetProperty(ref _calculationHasFeedIds, value);
        }

        private List<CalculationHasFeedEntity>? _calculationHasFeeds;

        public List<CalculationHasFeedEntity>? CalculationHasFeeds
        {
            get => _calculationHasFeeds;
            set => SetProperty(ref _calculationHasFeeds, value);
        }

        private List<int>? _calculationHasResultIds;

        public List<int>? CalculationHasResultIds
        {
            get => _calculationHasResultIds;
            set => SetProperty(ref _calculationHasResultIds, value);
        }

        private List<CalculationHasResultEntity>? _calculationHasResults;

        public List<CalculationHasResultEntity>? CalculationHasResults
        {
            get => _calculationHasResults;
            set => SetProperty(ref _calculationHasResults, value);
        }

        private List<StoredResults>? _storedResults;

        public List<StoredResults>? StoredResults
        {
            get => _storedResults;
            set => SetProperty(ref _storedResults, value);
        }

        #endregion Properties

        // Collections to hold options and feeds
        public ObservableCollection<FeedEntity> Feeds { get; set; } = new ObservableCollection<FeedEntity>();

        public ObservableCollection<LookupDTO> Types { get; set; } = new ObservableCollection<LookupDTO>();
        public ObservableCollection<SheepTypeEntity> SheepTypes { get; set; } = new ObservableCollection<SheepTypeEntity>();
        public ObservableCollection<GoatTypeEntity> GoatTypes { get; set; } = new ObservableCollection<GoatTypeEntity>();
        public ObservableCollection<GrazingEntity> Grazings { get; set; } = new ObservableCollection<GrazingEntity>();
        public ObservableCollection<BodyWeightEntity> BodyWeights { get; set; } = new ObservableCollection<BodyWeightEntity>();
        public ObservableCollection<DietQualityEstimateEntity> DietQualityEstimates { get; set; } = new ObservableCollection<DietQualityEstimateEntity>();
        public ObservableCollection<KidsLambsEntity> NrSucklingKidsLambs { get; set; } = new ObservableCollection<KidsLambsEntity>();

        // Method to load feeds asynchronously
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
                Console.WriteLine(ex.Message);
            }
        }

        // Method to load animal information asynchronously
        private async Task LoadAnimalInformationAsync()
        {
            try
            {
                Types.Clear();
                Grazings.Clear();
                BodyWeights.Clear();
                DietQualityEstimates.Clear();
                NrSucklingKidsLambs.Clear();

                // Load types
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

                // Load Grazings
                var grazings = await _baseService.EnumEntitiesService.GetGrazingsAsync();
                foreach (var grazing in grazings.Data)
                {
                    Grazings.Add(grazing);
                }

                // Load BodyWeights
                var bodyWeights = await _baseService.EnumEntitiesService.GetBodyWeightsAsync();
                foreach (var bodyWeight in bodyWeights.Data)
                {
                    BodyWeights.Add(bodyWeight);
                }

                // Load kids/lambs
                var kidsLambs = await _baseService.EnumEntitiesService.GetKidsLambsAsync();
                foreach (var kidsLamb in kidsLambs.Data)
                {
                    NrSucklingKidsLambs.Add(kidsLamb);
                }

                // Load DietQualityEstimates
                var dietQualityEstimates = await _baseService.EnumEntitiesService.GetDietQualityEstimatesAsync();
                foreach (var dietQualityEstimate in dietQualityEstimates.Data)
                {
                    DietQualityEstimates.Add(dietQualityEstimate);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"An error occurred while loading animal information: {ex.Message}");
            }
        }

        // Collection to hold stored feeds
        public ObservableCollection<StoredFeed> StoredFeeds { get; set; } = new ObservableCollection<StoredFeed>();

        // Method to add a feed to the stored feeds collection
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

                StoredFeeds.Insert(0, storedFeed); // Add new feed to the top of the list
                ClearAddedFeedForm(); // Clear form for next entry
                AddFeedBoxText = "Add additional feed";

                if (StoredFeeds.Count >= 3)
                {
                    IsResultsButtonVisible = true;
                    IsAddFeedExpanded = false;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"An error occurred while adding the feed: {ex.Message}");
            }
        }

        // Method to clear the added feed form
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

        // Method to clear current feed information
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

                var calculationId = _baseService.CalculationService.SaveCalculationAsync(animalInformation).Result.Data;
                return calculationId;
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"An error occurred while saving animal information: {ex.Message}");
                return 0; // Return a default value or handle it as needed
            }
        }

        private void ValidateCalculation(CalculationEntity calculation)
        {
            var results = _validator.Validate(calculation);
            ValidationErrors.Clear();

            // if IsNrSucklingsVisible is false the result for the kids/lams validation will be ignored
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

        // Method to get feed information inputs
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

                    calcHasFeedId = _baseService.CalculationService.SaveCalculationHasFeedAsync(calcFeed).Result.Data;
                    if (calcHasFeedId != 0)
                        calcHasFeedIds.Add(calcHasFeedId);
                }
                catch (Exception ex)
                {
                    // Handle the exception for individual feed input
                    Console.WriteLine($"An error occurred while saving feed input for feed ID {storedFeed.Feed?.Id}: {ex.Message}");
                }
            }
            CalculationHasFeeds = calcFeedList;

            return calcHasFeedIds;
        }

        // Method to perform calculation asynchronously
        private async Task DoCalculationAsync(int calculationId, List<int> calculationHasFeedIds)
        {
            CalculateResult(calculationId, calculationHasFeedIds);
        }

        public async void CalculateResult(int calculationId, List<int> calculationHasFeedIds)
        {
            try
            {
                decimal totalCost = 0;
                var calcFeedInformation = new List<CalculationHasFeedEntity>();
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
                        // Handle the exception for individual feed retrieval
                        Console.WriteLine($"An error occurred while retrieving calc has feed with ID {calcFeedId}: {ex.Message}");
                    }
                }

                var calcHasResultList = new List<CalculationHasResultEntity>();
                var storedResultList = new List<StoredResults>();
                // Calculate the result
                foreach (var info in calcFeedInformation)
                {
                    var dmig = info.Intake * info.DM / 100;
                    var cpig = dmig * info.CPDM / 100;
                    var meimjday = dmig * info.MEMJKGDM / 1000;
                    var cost = info.Intake * info.Price / 1000;

                    totalCost += cost;

                    var calcHasResult = new CalculationHasResultEntity
                    {
                        CalculationId = calculationId,
                        GFresh = 50,
                        PercentFresh = 50,
                        PercentDryMatter = 50,
                        TotalRation = cost
                    };

                    calcHasResultList.Add(calcHasResult);

                    // get feed by id
                    var feed = await _baseService.FeedService.GetById(info.FeedId);

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
                TotalRation = totalCost;
                CalculationHasResults = calcHasResultList;
                StoredResults = storedResultList;
            }
            catch (Exception ex)
            {
                // Handle the exception for the overall calculation process
                Console.WriteLine($"An error occurred while calculating the result: {ex.Message}");
            }
        }

        // Method to save feed to database
        private async void OnSaveResults()
        {
            try
            {
                // Show custom prompt page
                var promptPage = new SaveCalculationPrompt();
                await Application.Current.MainPage.Navigation.PushModalAsync(promptPage);

                // Subscribe to the message from the prompt page
                MessagingCenter.Subscribe<SaveCalculationPrompt, Tuple<string, string>>(this, "SaveCalculation", async (sender, result) =>
                {
                    try
                    {
                        var name = result.Item1;
                        var description = result.Item2;

                        // Save the calculation
                        var calcHasResultIds = new List<int>();
                        foreach (var calcHasResult in CalculationHasResults)
                        {
                            var calculation = await _baseService.CalculationService.GetCalculationById(calcHasResult.CalculationId);
                            if (calculation?.Data != null)
                            {
                                calculation.Data.UpdateNameAndDescription(name, description);
                                // Save the updated calculation
                                await _baseService.CalculationService.UpdateCalculationAsync(calculation.Data);

                                await _baseService.CalculationService.SaveCalculationHasResultAsync(calcHasResult);
                            }
                        }

                        // Optionally, display a message to the user
                        await Toast.Make("Results saved successfully.").Show();

                        // Unsubscribe from the message
                        MessagingCenter.Unsubscribe<SaveCalculationPrompt, Tuple<string, string>>(this, "SaveCalculation");

                        // Show custom popup with OK button
                        var customAlertPopup = new CustomAlertPopup("Save complete!", "Please use the view calculations option to view your saved calculation.");
                        await Application.Current.MainPage.ShowPopupAsync(customAlertPopup);
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (e.g., log it, show a message to the user, etc.)
                        Console.WriteLine($"An error occurred while saving the calculation: {ex.Message}");
                        await Toast.Make("An error occurred while saving the calculation.").Show();
                    }
                });
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"An error occurred while showing the prompt page: {ex.Message}");
                await Toast.Make("An error occurred while showing the prompt page.").Show();
            }
        }
    }
}