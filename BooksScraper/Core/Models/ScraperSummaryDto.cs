namespace BooksScraper.Core.Models
{
    public record ScraperSummaryDto(
    int TotalItemsFound,
    decimal AveragePrice,
    decimal MinPrice,
    decimal MaxPrice,
    int InStockCount,
    int OutOfStockCount,
    Dictionary<int, int> RatingDistribution
);
}
