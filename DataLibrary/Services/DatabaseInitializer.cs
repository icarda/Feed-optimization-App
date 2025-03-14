using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace DataLibrary.Services
{
    /// <summary>
    /// Service for initializing the database.
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInitializer"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public DatabaseInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Initializes the database by applying migrations and importing feeds from an embedded CSV file.
        /// </summary>
        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
                //await ImportFeedsFromEmbeddedCsvAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during initialization: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        /// <summary>
        /// Imports feed data from an embedded CSV file into the database.
        /// </summary>
        public async Task ImportFeedsFromEmbeddedCsvAsync(int countryId, int languageId)
        {
            Console.WriteLine("Importing feeds from CSV...");

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "";
            if (countryId == 1)
            {
                resourceName = "DataLibrary.english_ethiopia_feeds.csv";
            }
            if (countryId == 2)
            {
                if (languageId == 1)
                {
                    resourceName = "DataLibrary.english_tunisia_feeds.csv";
                }
                else
                {
                    resourceName = "DataLibrary.french_tunisia_feeds.csv";
                }
            }

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                Console.WriteLine("Resource not found.");
                return; // Handle the case where the resource is not found
            }

            var encoding = Encoding.GetEncoding("ISO-8859-1"); // Try different encodings
            using var reader = new StreamReader(stream, encoding);

            string csvContent = await reader.ReadToEndAsync();

            Console.WriteLine("CSV Content:");
            Console.WriteLine(csvContent);

            // Reset the stream position to the beginning
            stream.Position = 0;
            reader.DiscardBufferedData();

            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null, // Ignore missing fields
            });

            // Register the custom type converter
            csv.Context.TypeConverterCache.AddConverter<decimal>(new DecimalConverterWithDefault());

            csv.Context.RegisterClassMap<FeedMap>();
            var records = csv.GetRecords<FeedEntity>().ToList();
            Console.WriteLine($"Number of records read: {records.Count}");
            foreach (var record in records)
            {
                Console.WriteLine($"Record: {record.Name}");
            }

            foreach (var record in records)
            {
                await _context.Feeds.AddAsync(record);
            }
            await _context.SaveChangesAsync();
            Console.WriteLine("Feeds imported successfully.");
        }

        /// <summary>
        /// Clears and repopulates the FeedEntity table in the database.
        /// </summary>
        public async Task ClearAndRepopulateFeedsAsync(int countryId, int languageId)
        {
            Console.WriteLine("Clearing and repopulating feeds...");

            // Clear the FeedEntity table
            _context.Feeds.RemoveRange(_context.Feeds);
            await _context.SaveChangesAsync();

            // Repopulate the FeedEntity table
            await ImportFeedsFromEmbeddedCsvAsync(countryId, languageId);
        }

        /// <summary>
        /// Custom type converter for decimal values that defaults empty values to 0.
        /// </summary>
        private class DecimalConverterWithDefault : DecimalConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return 0m; // Return 0 if the value is empty
                }
                return base.ConvertFromString(text, row, memberMapData);
            }
        }
    }
}