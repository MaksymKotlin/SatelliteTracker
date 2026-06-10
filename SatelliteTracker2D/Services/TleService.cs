using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SatelliteTracker2D.Services;

public class TleService
{
    private const string TleUrl = "https://api.wheretheiss.at/v1/satellites/25544/tles";
    private static readonly HttpClient HttpClient = new();

    private string[] _cachedLines = [];
    private DateTime _lastFetch = DateTime.MinValue;

    public async Task<string[]> GetTleLinesAsync()
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
        return _cachedLines;
    }
}