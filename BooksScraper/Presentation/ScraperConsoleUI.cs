using BooksScraper.Core.Models;

namespace BooksScraper.Presentation;

public static class ScraperConsoleUI
{
    public static void RenderHeader()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("   Universal E-Commerce Scraper (.NET 8)          ");
        Console.WriteLine("==================================================\n");
    }

    public static ScraperConfiguration GetConfigurationFromUser()
    {
        Console.Write("Enter target URL [Default: https://books.toscrape.com/]: ");
        string? inputUrl = Console.ReadLine()?.Trim();
        string baseUrl = string.IsNullOrEmpty(inputUrl) ? "https://books.toscrape.com/" : inputUrl;

        Console.Write("Enter max pages to scrape [Default: 3]: ");
        string? inputPages = Console.ReadLine()?.Trim();
        int maxPages = int.TryParse(inputPages, out var p) && p > 0 ? p : 3;

        return new ScraperConfiguration
        {
            BaseUrl = baseUrl,
            MaxPagesToScrape = maxPages
        };
    }

    public static void RenderSummary(ScraperSummaryDto summary)
    {
        Console.WriteLine($"\n--- SUMMARY STATISTICS ---");
        Console.WriteLine($"Total Items Found: {summary.TotalItemsFound}");
        Console.WriteLine($"Average Price:     £{summary.AveragePrice}");
        Console.WriteLine($"Price Range:       £{summary.MinPrice} - £{summary.MaxPrice}");
        Console.WriteLine($"In Stock:          {summary.InStockCount} pcs");
        Console.WriteLine($"Out of Stock:      {summary.OutOfStockCount} pcs");
    }

    public static string GetExportFormatChoice()
    {
        Console.WriteLine("\nSelect export format:");
        Console.WriteLine("1) JSON");
        Console.WriteLine("2) CSV");
        Console.Write("Choice (1 or 2, default 1): ");
        return Console.ReadLine()?.Trim() ?? "1";
    }

    public static void RenderMessage(string message, ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}