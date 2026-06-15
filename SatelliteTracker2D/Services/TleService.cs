using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SatelliteTracker2D.Services;

public class TleService
{
    private const string TleUrl = "https://tle.ivanstanojevic.me/api/tle/25544";
    private static readonly HttpClient HttpClient = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private string[] _cachedLines = [];
    private DateTime _lastFetch = DateTime.MinValue;


    public async Task<string[]> GetTleLinesAsync()
    {
        if (_cachedLines.Length > 0 && (DateTime.UtcNow - _lastFetch).TotalHours < 1)
            return _cachedLines;

        await _semaphore.WaitAsync();
        try
        {
            if (_cachedLines.Length > 0 && (DateTime.UtcNow - _lastFetch).TotalHours < 1)
                return _cachedLines;

            string response = await HttpClient.GetStringAsync(TleUrl);
            using JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;

            _cachedLines =
            [
                root.GetProperty("name").GetString()!,
                root.GetProperty("line1").GetString()!,
                root.GetProperty("line2").GetString()!
            ];

            _lastFetch = DateTime.UtcNow;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network error fetching TLE: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Json is in incorrect format: {ex.Message}");
        }
        finally
        {
            _semaphore.Release();
        }

        return _cachedLines;
    }
}
