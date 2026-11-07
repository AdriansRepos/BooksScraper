namespace BooksScraper.Core.Models
{
    public class ScraperConfiguration
{
    public string BaseUrl { get; set; } = string.Empty;
    public int MaxPagesToScrape { get; set; } = 3;
    public decimal? MaxPriceFilter { get; set; }
    public int? MinRatingFilter { get; set; }
}
}
