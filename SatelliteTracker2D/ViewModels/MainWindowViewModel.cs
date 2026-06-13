using System;
using System.Threading.Tasks;
using System.Timers;

using CommunityToolkit.Mvvm.ComponentModel;

using SatelliteTracker2D.Services;

namespace SatelliteTracker2D.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly TleService _tleService = new();
    private readonly IssPredictor _issPredictor = new();
    private readonly Timer _timer = new(1000);

    [ObservableProperty] private double _latitude;

    [ObservableProperty] private double _longitude;

    [ObservableProperty] private double _speed;

    public MainWindowViewModel()
    {
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        string[] tleLines = await _tleService.GetTleLinesAsync();
        _issPredictor.LoadTle(tleLines);

        _timer.Start();
    }

    private async void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _timer.Stop();
        try
        {
            string[] tleLines = await _tleService.GetTleLinesAsync();
            _issPredictor.LoadTle(tleLines);

            (Latitude, Longitude, Speed) = _issPredictor.GetCurrentPosition();
        }
        catch
        {
        }
        finally
        {
            _timer.Start();
        }

        Console.WriteLine($"ISS → Lat: {Latitude:F4}°  Lon: {Longitude:F4}°");
    }
}
