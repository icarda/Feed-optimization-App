using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

        /// <summary>
        /// Loads a disclaimer from an embedded Word document resource and converts it to HTML.
        /// </summary>
        /// <returns>A string containing the disclaimer in HTML format, or an error message if the resource cannot be loaded.</returns>
        public string LoadDisclaimerFromEmbeddedResource()
        {
            try
            {
                // Get the assembly containing the embedded resources.
                var assembly = Assembly.GetExecutingAssembly();

                // Specify the name of the embedded Word document resource.
                var resourceName = "DataLibrary.disclaimer.docx";

                // Get the embedded resource stream.
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                {
                    // Log a message and return an error message if the resource is not found.
                    Console.WriteLine("Resource not found.");
                    return "<p>Disclaimer file not found.</p>";
                }

                // Open the Word document from the resource stream in read-only mode.
                using (var wordDoc = WordprocessingDocument.Open(stream, false))
                {
                    // Get the body of the Word document.
                    var body = wordDoc.MainDocumentPart.Document.Body;

                    // Convert the Word document body to HTML and return it.
                    return ConvertWordToHtml(body);
                }
            }
            catch (Exception ex)
            {
                // Return a generic error message if an exception occurs.
                return "<p>An error occurred while loading the disclaimer.</p>";
            }
        }

        /// <summary>
        /// Converts the content of a Word document body to HTML format.
        /// </summary>
        /// <param name="body">The body of the Word document to convert.</param>
        /// <returns>A string containing the HTML representation of the Word document body.</returns>
        private string ConvertWordToHtml(Body body)
        {
            // Initialize a StringBuilder to construct the HTML content.
            var htmlBuilder = new StringBuilder();

            // Iterate through the elements in the Word document body.
            foreach (var element in body.Elements())
            {
                if (element is Paragraph paragraph)
                {
                    // Convert a paragraph to an HTML <p> element.
                    htmlBuilder.Append("<p>");
                    foreach (var run in paragraph.Elements<Run>())
                    {
                        // Get the text content of the run.
                        var text = run.GetFirstChild<Text>()?.Text;

                        // Apply formatting based on the run's properties (e.g., bold, italic).
                        if (run.RunProperties?.Bold != null)
                        {
                            htmlBuilder.Append($"<b>{text}</b>");
                        }
                        else if (run.RunProperties?.Italic != null)
                        {
                            htmlBuilder.Append($"<i>{text}</i>");
                        }
                        else
                        {
                            htmlBuilder.Append(text);
                        }
                    }
                    htmlBuilder.Append("</p>");
                }
                else if (element is Table table)
                {
                    // Convert a table to an HTML <table> element.
                    htmlBuilder.Append("<table border='1'>");
                    foreach (var row in table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>())
                    {
                        htmlBuilder.Append("<tr>");
                        foreach (var cell in row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>())
                        {
                            htmlBuilder.Append("<td>");
                            // Recursively convert the content of the table cell to HTML.
                            htmlBuilder.Append(ConvertWordToHtml(cell.GetFirstChild<Body>()));
                            htmlBuilder.Append("</td>");
                        }
                        htmlBuilder.Append("</tr>");
                    }
                    htmlBuilder.Append("</table>");
                }
            }

            // Return the constructed HTML content as a string.
            return htmlBuilder.ToString();
        }

        /// <summary>
        /// Clears all calculation-related data from the database, including calculations, feeds, and results.
        /// </summary>
        public async Task ClearAllCalculationsAsync()
        {
            Console.WriteLine("Clearing calculations...");

            // Clear the CalculationEntity table.
            _context.Calculations.RemoveRange(_context.Calculations);
            await _context.SaveChangesAsync();

            // Clear the CalculationHasFeedEntity table.
            _context.CalculationHasFeeds.RemoveRange(_context.CalculationHasFeeds);
            await _context.SaveChangesAsync();

            // Clear the CalculationHasResultsEntity table.
            _context.CalculationHasResults.RemoveRange(_context.CalculationHasResults);
            await _context.SaveChangesAsync();

            Console.WriteLine("Calculations cleared successfully.");
        }
    }
}