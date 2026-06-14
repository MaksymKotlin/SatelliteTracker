using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Interactivity;
using SatelliteTracker2D.ViewModels;

namespace SatelliteTracker2D.Views;

public partial class MainWindow : Window
{
    private const double CanvasWidth = 1440;
    private const double CanvasHeight = 720;
    private const double IconWidth = 80;
    private const double IconHeight = 50;

    public MainWindow()
    {
        InitializeComponent();

        var vm = new MainWindowViewModel();
        DataContext = vm;
        PredictButton.Click += OnPredictClick;
        vm.PropertyChanged += OnViewModelPropertyChanged;
        ResetButton.Click += (_, _) => _isPredicting = false;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not (nameof(MainWindowViewModel.Latitude) or nameof(MainWindowViewModel.Longitude)))
            return;

        var vm = (MainWindowViewModel)sender!;

        Dispatcher.UIThread.Post(() => UpdateIssPosition(vm.Latitude, vm.Longitude, vm.Speed));
    }

    private bool _isPredicting = false;
    private double _predictMinutes = 0;

    private void OnPredictClick(object? sender, RoutedEventArgs e)
    {
        if (!double.TryParse(MinutesInput.Text, out double minutes))
            return;

        _predictMinutes = minutes;
        _isPredicting = true;
    }

    private void UpdateIssPosition(double latitude, double longitude, double speed)
    {
        var vm = (MainWindowViewModel)DataContext!;

        if (_isPredicting)
        {
            (latitude, longitude, speed) = vm.GetPredictedPosition(_predictMinutes);
        }

        double x = (longitude + 180.0) / 360.0 * CanvasWidth - IconWidth / 2;
        double y = (90.0 - latitude) / 180.0 * CanvasHeight - IconHeight / 2;

        Canvas.SetLeft(IssIcon, x);
        Canvas.SetTop(IssIcon, y);

        LongitudeText.Text = $"Longitude: {longitude:F4}°";
        LatitudeText.Text  = $"Latitude: {latitude:F4}°";
        SpeedText.Text     = $"Speed: {speed:F2} km/s";
    }
}
