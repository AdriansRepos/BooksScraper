using BooksScraper.Core.Models;

namespace BooksScraper.Core.Abstractions;

public interface IWebScraperEngine
{
    Task<IEnumerable<ScrapedItemDto>> ScrapeAsync(ScraperConfiguration config, IScraperStrategy strategy, 
        CancellationToken cancellationToken = default);
}
