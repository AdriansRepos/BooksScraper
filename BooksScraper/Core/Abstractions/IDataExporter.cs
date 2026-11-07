using BooksScraper.Core.Models;

namespace BooksScraper.Core.Abstractions;

public interface IDataExporter
{
    Task ExportAsync<T>(IEnumerable<T> data, string filePath, CancellationToken cancellationToken = default);
}
