using BooksScraper.Core.Abstractions;
using BooksScraper.Infrastructure;
using BooksScraper.Infrastructure.Exporters;
using BooksScraper.Infrastructure.Strategies;
using BooksScraper.Presentation;

namespace BooksScraper.Services;

public class ScraperApp
{
    public static async Task RunAsync()
    {
        // 1. Displaying the header and loading configuration from the UI
        ScraperConsoleUI.RenderHeader();
        var config = ScraperConsoleUI.GetConfigurationFromUser();

        // 2. Dependency initialization
        using var httpClient = new HttpClient();
        IWebScraperEngine engine = new AngleSharpEngine(httpClient);
        IScraperStrategy strategy = new BooksToScrapeStrategy();

        // 3. Starting the scraping process
        ScraperConsoleUI.RenderMessage($"\n[1/4] Scraping first {config.MaxPagesToScrape} pages from {config.BaseUrl}...", ConsoleColor.Yellow);
        var scrapedItems = (await engine.ScrapeAsync(config, strategy)).ToList();

        if (scrapedItems.Count == 0)
        {
            ScraperConsoleUI.RenderMessage("[Error] No items were found or the page could not be reached.", ConsoleColor.Red);
            return;
        }

        ScraperConsoleUI.RenderMessage($"[2/4] Successfully scraped {scrapedItems.Count} items.");

        // 4. Calculation of statistics
        var summary = ScraperService.GenerateSummary(scrapedItems);
        ScraperConsoleUI.RenderSummary(summary);

        // 5. Selection and execution of export
        string choice = ScraperConsoleUI.GetExportFormatChoice();
        IDataExporter exporter = choice == "2" ? new CsvDataExporter() : new JsonDataExporter();
        string extension = choice == "2" ? "csv" : "json";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), $"books_export.{extension}");

        ScraperConsoleUI.RenderMessage($"\n[4/4] Exporting data to {outputPath}...", ConsoleColor.Yellow);
        await exporter.ExportAsync(scrapedItems, outputPath);

        ScraperConsoleUI.RenderMessage("\n[SUCCESS] Operation completed smoothly!");
    }
}