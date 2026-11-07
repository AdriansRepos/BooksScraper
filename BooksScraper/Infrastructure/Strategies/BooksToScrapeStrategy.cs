using AngleSharp.Html.Parser;
using BooksScraper.Core.Abstractions;
using BooksScraper.Core.Models;

namespace BooksScraper.Infrastructure.Strategies;

public class BooksToScrapeStrategy : IScraperStrategy
{
    private readonly HtmlParser _parser = new();

    public string TargetDomain => "books.toscrape.com";

    public string BuildPageUrl(string baseUrl, int pageIndex)
    {
        // Pagination on books.toscrape.com: catalogue/page-1.html, catalogue/page-2.html
        return pageIndex == 1 
            ? baseUrl 
            : $"{baseUrl.TrimEnd('/')}/catalogue/page-{pageIndex}.html";
    }

    public IEnumerable<ScrapedItemDto> ParsePage(string htmlContent)
    {
        var document = _parser.ParseDocument(htmlContent);
        var productElements = document.QuerySelectorAll("article.product_pod");

        var items = new List<ScrapedItemDto>();

        foreach (var element in productElements)
        {
            // Title extraction
            var titleElement = element.QuerySelector("h3 a");
            string title = titleElement?.GetAttribute("title") ?? titleElement?.TextContent ?? "Unknown Title";

            // Price extraction (e.g., "£51.77")
            var priceText = element.QuerySelector(".price_color")?.TextContent ?? "£0";
            decimal price = ParsePrice(priceText);
            string currency = priceText.StartsWith("£") ? "GBP" : "EUR";

            // Extracting ratings from word classes (Star-rating One/Two/Three/Four/Five)
            var ratingElement = element.QuerySelector(".star-rating");
            int rating = ParseRating(ratingElement?.ClassList);

            // Extraction of shelf life data
            var availabilityText = element.QuerySelector(".availability")?.TextContent ?? "";
            bool isInStock = availabilityText.Contains("In stock", StringComparison.OrdinalIgnoreCase);

            // Relative URL
            string relativeUrl = titleElement?.GetAttribute("href") ?? "";

            items.Add(new ScrapedItemDto(
                Title: title,
                Price: price,
                Currency: currency,
                RatingStars: rating,
                IsInStock: isInStock,
                ProductUrl: relativeUrl
            ));
        }

        return items;
    }

    private static decimal ParsePrice(string priceText)
    {
        var cleanPrice = new string([.. priceText.Where(c => char.IsDigit(c) || c == '.')]);
        return decimal.TryParse(cleanPrice, System.Globalization.CultureInfo.InvariantCulture, out var result) ? result : 0m;
    }

    private static int ParseRating(IEnumerable<string>? classList)
    {
        if (classList == null) return 0;
        if (classList.Contains("One")) return 1;
        if (classList.Contains("Two")) return 2;
        if (classList.Contains("Three")) return 3;
        if (classList.Contains("Four")) return 4;
        if (classList.Contains("Five")) return 5;
        return 0;
    }
}
