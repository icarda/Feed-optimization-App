using FeedOptimizationApp.Database;
using Microsoft.EntityFrameworkCore;

namespace FeedOptimizationApp
{
    public partial class MainPage : ContentPage
    {
        private readonly ApplicationDbContext _context;
        private int count = 0;

        public MainPage(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var feeds = await _context.Feeds.ToListAsync();
            if (feeds.Count == 0)
            {
                Console.WriteLine("No feeds found.");
                return;
            }
            FeedCountLbl.Text = $"Number of Feeds: {feeds.Count}";
            Console.WriteLine($"Feeds count: {feeds.Count}");
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}