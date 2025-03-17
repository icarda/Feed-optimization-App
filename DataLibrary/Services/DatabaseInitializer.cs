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
                // Apply any pending migrations to the database.
                await _context.Database.MigrateAsync();
                // Uncomment the following line to import feeds during initialization.
                // await ImportFeedsFromEmbeddedCsvAsync();
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during initialization.
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
        /// <param name="countryId">The ID of the country for which to import feeds.</param>
        /// <param name="languageId">The ID of the language for which to import feeds.</param>
        public async Task ImportFeedsFromEmbeddedCsvAsync(int countryId, int languageId)
        {
            Console.WriteLine("Importing feeds from CSV...");

            // Get the assembly containing the embedded CSV files.
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "";

            // Determine the resource name based on the country and language IDs.
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

            // Get the embedded resource stream.
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                Console.WriteLine("Resource not found.");
                return; // Handle the case where the resource is not found.
            }

            // Read the CSV file using the specified encoding.
            var encoding = Encoding.GetEncoding("ISO-8859-1");
            using var reader = new StreamReader(stream, encoding);

            // Read the entire CSV content for logging purposes.
            string csvContent = await reader.ReadToEndAsync();
            Console.WriteLine("CSV Content:");
            Console.WriteLine(csvContent);

            // Reset the stream position to the beginning.
            stream.Position = 0;
            reader.DiscardBufferedData();

            // Configure the CSV reader.
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null, // Ignore missing fields.
            });

            // Register the custom type converter for decimal values.
            csv.Context.TypeConverterCache.AddConverter<decimal>(new DecimalConverterWithDefault());

            // Register the class map for FeedEntity.
            csv.Context.RegisterClassMap<FeedMap>();

            // Read the records from the CSV file.
            var records = csv.GetRecords<FeedEntity>().ToList();
            Console.WriteLine($"Number of records read: {records.Count}");
            foreach (var record in records)
            {
                Console.WriteLine($"Record: {record.Name}");
            }

            // Add the records to the database context.
            foreach (var record in records)
            {
                await _context.Feeds.AddAsync(record);
            }

            // Save the changes to the database.
            await _context.SaveChangesAsync();
            Console.WriteLine("Feeds imported successfully.");
        }

        /// <summary>
        /// Clears and repopulates the FeedEntity table in the database.
        /// </summary>
        /// <param name="countryId">The ID of the country for which to repopulate feeds.</param>
        /// <param name="languageId">The ID of the language for which to repopulate feeds.</param>
        public async Task ClearAndRepopulateFeedsAsync(int countryId, int languageId)
        {
            Console.WriteLine("Clearing and repopulating feeds...");

            // Clear the FeedEntity table.
            _context.Feeds.RemoveRange(_context.Feeds);
            await _context.SaveChangesAsync();

            // Repopulate the FeedEntity table.
            await ImportFeedsFromEmbeddedCsvAsync(countryId, languageId);
        }

        /// <summary>
        /// Clears the FeedEntity table in the database.
        /// </summary>
        public async Task ClearFeedsAsync()
        {
            Console.WriteLine("Clearing feeds...");

            // Clear the FeedEntity table.
            _context.Feeds.RemoveRange(_context.Feeds);
            await _context.SaveChangesAsync();

            Console.WriteLine("Feeds cleared successfully.");
        }

        /// <summary>
        /// Custom type converter for decimal values that defaults empty values to 0.
        /// </summary>
        private class DecimalConverterWithDefault : DecimalConverter
        {
            /// <summary>
            /// Converts a string to a decimal value, defaulting to 0 if the string is empty.
            /// </summary>
            /// <param name="text">The text to convert.</param>
            /// <param name="row">The current row being read.</param>
            /// <param name="memberMapData">The member map data.</param>
            /// <returns>The converted decimal value.</returns>
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return 0m; // Return 0 if the value is empty.
                }
                return base.ConvertFromString(text, row, memberMapData);
            }
        }
    }
}