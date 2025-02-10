using DataLibrary.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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