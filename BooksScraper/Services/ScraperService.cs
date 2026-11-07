using BooksScraper.Core.Models;

namespace BooksScraper.Services;

public class ScraperService
{
    public static IEnumerable<ScrapedItemDto> ApplyFilters(IEnumerable<ScrapedItemDto> items, decimal? maxPrice, int? minRating)
    {
        return items
            .Where(i => !maxPrice.HasValue || i.Price <= maxPrice.Value)
            .Where(i => !minRating.HasValue || i.RatingStars >= minRating.Value)
            .OrderByDescending(i => i.RatingStars)
            .ThenBy(i => i.Price);
    }

    public static ScraperSummaryDto GenerateSummary(IEnumerable<ScrapedItemDto> items)
    {
        var itemList = items.ToList();

        if (itemList.Count == 0)
            return new ScraperSummaryDto(0, 0, 0, 0, 0, 0, []);

        return new ScraperSummaryDto(
            TotalItemsFound: itemList.Count,
            AveragePrice: Math.Round(itemList.Average(i => i.Price), 2),
            MinPrice: itemList.Min(i => i.Price),
            MaxPrice: itemList.Max(i => i.Price),
            InStockCount: itemList.Count(i => i.IsInStock),
            OutOfStockCount: itemList.Count(i => !i.IsInStock),
            RatingDistribution: itemList
                .GroupBy(i => i.RatingStars)
                .ToDictionary(g => g.Key, g => g.Count())
        );
    }
}
