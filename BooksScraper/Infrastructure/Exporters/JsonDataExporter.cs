using AngleSharp.Html.Parser;
using BooksScraper.Core.Abstractions;
using BooksScraper.Core.Models;
using System.Text.Json;
using System.Text;

namespace BooksScraper.Infrastructure.Exporters;

public class JsonDataExporter : IDataExporter
{
    // Statická mezipaměť (cache) instance pro celou aplikaci
    private static readonly JsonSerializerOptions _cachedOptions = new()
    {
        WriteIndented = true
    };

    public async Task ExportAsync<T>(IEnumerable<T> data, string filePath, CancellationToken cancellationToken = default)
    {
        await using var stream = File.Create(filePath);
        // Použijeme předpřipravenou instanci z mezipaměti
        await JsonSerializer.SerializeAsync(stream, data, _cachedOptions, cancellationToken);
    }
}