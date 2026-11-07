using BooksScraper.Core.Models;

namespace BooksScraper.Core.Abstractions;

public interface IScraperStrategy
{
    string TargetDomain { get; }
    string BuildPageUrl(string baseUrl, int pageIndex);
    IEnumerable<ScrapedItemDto> ParsePage(string htmlContent);
}
