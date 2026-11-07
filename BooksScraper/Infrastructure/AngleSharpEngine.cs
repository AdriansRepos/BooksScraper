using AngleSharp.Html.Parser;
using BooksScraper.Core.Abstractions;
using BooksScraper.Core.Models;
using System.Text.Json;
using System.Text;

namespace BooksScraper.Infrastructure;

public class AngleSharpEngine : IWebScraperEngine
{
    private readonly HttpClient _httpClient;

    public AngleSharpEngine(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) EnterpriseScraper/1.0");
    }

    public async Task<IEnumerable<ScrapedItemDto>> ScrapeAsync(
        ScraperConfiguration config, 
        IScraperStrategy strategy, 
        CancellationToken cancellationToken = default)
    {
        var results = new List<ScrapedItemDto>();

        for (int page = 1; page <= config.MaxPagesToScrape; page++)
        {
            string url = strategy.BuildPageUrl(config.BaseUrl, page);
            
            try
            {
                var response = await _httpClient.GetAsync(url, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Warning] Failed to retrieve page {page}. Status Code: {response.StatusCode}");
                    break;
                }

                string html = await response.Content.ReadAsStringAsync(cancellationToken);
                var items = strategy.ParsePage(html);
                results.AddRange(items);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Exception on page {page}: {ex.Message}");
                break;
            }
        }

        return results;
    }
}