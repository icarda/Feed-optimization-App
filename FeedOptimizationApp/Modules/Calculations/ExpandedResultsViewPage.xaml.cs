
namespace FeedOptimizationApp.Modules.Calculations
{
    public partial class ExpandedResultsViewPage : ContentPage
    {
        public ExpandedResultsViewPage(ExpandedResultsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}