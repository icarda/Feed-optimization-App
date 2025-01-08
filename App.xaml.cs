using FeedOptimizationApp.Database;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;
using FeedOptimizationApp.Data.Models;

namespace FeedOptimizationApp
{
    public partial class App : Application
    {
        private readonly ApplicationDbContext _context;

        public App(ApplicationDbContext context)
        {
            InitializeComponent();

            _context = context;
            context.Database.Migrate();

            ImportFeedsFromEmbeddedCsvAsync().Wait();

            MainPage = new AppShell();
        }

        public async Task ImportFeedsFromEmbeddedCsvAsync()
        {
            Console.WriteLine("Importing feeds from CSV...");
            // Check if the data already exists
            if (await _context.Feeds.AnyAsync())
            {
                Console.WriteLine("Data already exists, no need to import.");
                return; // Data already exists, no need to import
            }

            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                Console.WriteLine(resourceName);
            }
            var resourceName1 = "FeedOptimizationApp.Data.feeds.csv"; // Update with the actual resource name

            using var stream = assembly.GetManifestResourceStream(resourceName1);
            if (stream == null)
            {
                Console.WriteLine("Resource not found.");
                return; // Handle the case where the resource is not found
            }

            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            });

            csv.Context.RegisterClassMap<FeedMap>();
            var records = csv.GetRecords<Feed>().ToList();
            foreach (var record in records)
            {
                record.Id = Guid.NewGuid(); // Ensure each record has a unique Id
                await _context.Feeds.AddAsync(record);
            }
            await _context.SaveChangesAsync();
            Console.WriteLine("Feeds imported successfully.");
        }
    }
}