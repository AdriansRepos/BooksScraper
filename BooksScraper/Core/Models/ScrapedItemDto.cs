namespace BooksScraper.Core.Models
{
    public record ScrapedItemDto(
    string Title,
    decimal Price,
    string Currency,
    int RatingStars,
    bool IsInStock,
    string ProductUrl,
    string? Category = null,
    Dictionary<string, string>? Attributes = null
);
}
