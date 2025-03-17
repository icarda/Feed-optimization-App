using DataLibrary.Models;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Calculations
{
    /// <summary>
    /// ViewModel for displaying a list of saved calculations.
    /// It retrieves calculation results, groups them, and prepares a display model.
    /// Also provides navigation to an expanded view of a selected calculation.
    /// </summary>
    public class ViewCalculationsViewModel : BaseViewModel
    {
        // Reference to the base service used for data operations.
        private readonly BaseService _baseService;

        /// <summary>
        /// List of all CalculationHasResultEntity records retrieved from the service.
        /// </summary>
        public List<CalculationHasResultEntity> CalculationHasResults { get; set; } = new();

        // Backing field for ordered calculation results.
        private ObservableCollection<CalculationHasResultEntity> _orderedCalculationHasResults;

        /// <summary>
        /// Ordered collection of calculation result entities.
        /// This property can be used for displaying sorted calculation results.
        /// </summary>
        public ObservableCollection<CalculationHasResultEntity> OrderedCalculationHasResults
        {
            get => _orderedCalculationHasResults;
            set => SetProperty(ref _orderedCalculationHasResults, value);
        }

        // Backing field for the list of calculation display models.
        private ObservableCollection<CalculationDisplayModel> _calculationsDisplayList;

        /// <summary>
        /// Collection of calculations prepared for display in the UI.
        /// Each item contains summary details such as title, date, number of feeds, and species type.
        /// </summary>
        public ObservableCollection<CalculationDisplayModel> CalculationsDisplayList
        {
            get => _calculationsDisplayList;
            set => SetProperty(ref _calculationsDisplayList, value);
        }

        /// <summary>
        /// Command executed when the user chooses to view the expanded details of a calculation.
        /// </summary>
        public ICommand ExpandViewCommand { get; }

        /// <summary>
        /// Initializes a new instance of the ViewCalculationsViewModel.
        /// Sets up commands and loads the list of calculations.
        /// </summary>
        /// <param name="baseService">Service for accessing data.</param>
        /// <param name="sharedData">Shared data context across the application.</param>
        public ViewCalculationsViewModel(BaseService baseService, SharedData sharedData)
            : base(sharedData)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            // Initialize the command to expand a calculation view.
            ExpandViewCommand = new Command(OnExpandView);
            // Load the list of calculations.
            LoadCalculations();
        }

        /// <summary>
        /// Command handler to expand the view of a selected calculation.
        /// Navigates to the expanded results page for the provided calculation ID.
        /// </summary>
        /// <param name="parameter">The calculation ID to expand (expected to be an integer).</param>
        private async void OnExpandView(object parameter)
        {
            if (parameter is int calculationId)
            {
                // Set the selected calculation ID in shared data.
                SharedData.CalculationId = calculationId;
                // Create a new view model for the expanded results.
                var viewModel = new ExpandedResultsViewModel(_baseService, SharedData);
                // Navigate to the expanded results page.
                await Application.Current.MainPage.Navigation.PushAsync(new ExpandedResultsViewPage(viewModel));
            }
        }

        /// <summary>
        /// Loads all calculations by fetching calculation results, grouping them by calculation ID,
        /// and then creating a display model for each group.
        /// </summary>
        public async void LoadCalculations()
        {
            try
            {
                // Fetch all calculation result records.
                var result = await _baseService.CalculationService.GetAllCalculationHasResults();
                CalculationHasResults = result.Data;

                // Group the results by CalculationId.
                var groupedResults = CalculationHasResults.GroupBy(x => x.CalculationId);

                // Create an observable collection to hold display models.
                var displayList = new ObservableCollection<CalculationDisplayModel>();

                // Process each group to prepare a summary display model.
                foreach (var group in groupedResults)
                {
                    var calculationId = group.Key;
                    // Retrieve detailed calculation information by its ID.
                    var calculation = await _baseService.CalculationService.GetCalculationById(calculationId);
                    var calculationName = calculation.Data.Name;
                    var calculationType = calculation.Data.Type;
                    var calculationDate = calculation.Data.CreatedDate;
                    // Retrieve the number of feeds used in this calculation.
                    var numberOfFeeds = _baseService.CalculationService.GetNumberOfFeedsInCalculationHasFeedByCalculationId(calculationId).Result.Data;

                    // Create and add a new display model item.
                    displayList.Add(new CalculationDisplayModel
                    {
                        CalculationId = calculationId,
                        CalculationTitle = calculationName,
                        CalculationDate = calculationDate.ToString("yyyy-MM-dd"), // Using the current date as a placeholder.
                        CalculationNrOfFeeds = numberOfFeeds.ToString(),
                        CalculationSpeciesType = calculationType
                    });
                }

                // Update the CalculationsDisplayList property with the newly created list.
                CalculationsDisplayList = displayList;
            }
            catch (Exception ex)
            {
                // Log or handle any errors that occur while loading calculations.
                Console.WriteLine($"An error occurred while loading calculations: {ex.Message}");
            }
        }

        /// <summary>
        /// Model representing the display information for a single calculation.
        /// </summary>
        public class CalculationDisplayModel
        {
            /// <summary>
            /// The unique ID of the calculation.
            /// </summary>
            public int CalculationId { get; set; }

            /// <summary>
            /// The title or name of the calculation.
            /// </summary>
            public string CalculationTitle { get; set; }

            /// <summary>
            /// The date the calculation was performed or displayed.
            /// </summary>
            public string CalculationDate { get; set; }

            /// <summary>
            /// The number of feeds included in the calculation.
            /// </summary>
            public string CalculationNrOfFeeds { get; set; }

            /// <summary>
            /// The species type associated with the calculation (e.g., sheep, goat).
            /// </summary>
            public string CalculationSpeciesType { get; set; }
        }
    }
}