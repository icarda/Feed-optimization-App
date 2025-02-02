using CsvHelper;

using CsvHelper.Configuration;
using DataLibrary.DTOs;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

namespace DataLibrary.Services;

public class DatabaseInitializer
{
    private readonly ApplicationDbContext _context;

    public DatabaseInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
            await ImportFeedsFromEmbeddedCsvAsync();
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

    private async Task ImportFeedsFromEmbeddedCsvAsync()
    {
        Console.WriteLine("Importing feeds from CSV...");
        // Check if the data already exists
        if (await _context.Feeds.AnyAsync())
        {
            Console.WriteLine("Data already exists, no need to import.");
            return; // Data already exists, no need to import
        }

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "DataLibrary.feeds.csv"; // Update with the actual resource name

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            Console.WriteLine("Resource not found.");
            return; // Handle the case where the resource is not found
        }

        using var reader = new StreamReader(stream);
        string csvContent = await reader.ReadToEndAsync();
        Console.WriteLine("CSV Content:");
        Console.WriteLine(csvContent);

        // Reset the stream position to the beginning
        stream.Position = 0;
        reader.DiscardBufferedData();

        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

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
}