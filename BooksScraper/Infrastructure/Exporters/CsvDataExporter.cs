using AngleSharp.Html.Parser;
using BooksScraper.Core.Abstractions;
using BooksScraper.Core.Models;
using System.Text.Json;
using System.Text;

namespace BooksScraper.Infrastructure.Exporters;

public class CsvDataExporter : IDataExporter
{
    public async Task ExportAsync<T>(IEnumerable<T> data, string filePath, CancellationToken cancellationToken = default)
    {
        var sb = new StringBuilder();
        var properties = typeof(T).GetProperties();

        // Header
        sb.AppendLine(string.Join(",", properties.Select(p => $"\"{p.Name}\"")));

        // Rows
        foreach (var item in data)
        {
            var values = properties.Select(p =>
            {
                var val = p.GetValue(item)?.ToString() ?? "";
                return $"\"{val.Replace("\"", "\"\"")}\"";
            });
            sb.AppendLine(string.Join(",", values));
        }

        await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.UTF8, cancellationToken);
    }
}