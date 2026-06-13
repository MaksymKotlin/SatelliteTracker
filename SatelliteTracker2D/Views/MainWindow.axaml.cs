using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Threading;
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

        vm.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not (nameof(MainWindowViewModel.Latitude) or nameof(MainWindowViewModel.Longitude)))
            return;

        var vm = (MainWindowViewModel)sender!;

        Dispatcher.UIThread.Post(() => UpdateIssPosition(vm.Latitude, vm.Longitude, vm.Speed));
    }

    private void UpdateIssPosition(double latitude, double longitude, double speed)
    {
        double x = (longitude + 180.0) / 360.0 * CanvasWidth - IconWidth / 2;
        double y = (90.0 - latitude) / 180.0 * CanvasHeight - IconHeight / 2;

        Canvas.SetLeft(IssIcon, x);
        Canvas.SetTop(IssIcon, y);

        LongitudeText.Text = $"Longitude: {longitude:F4}°";
        LatitudeText.Text  = $"Latitude: {latitude:F4}°";
        SpeedText.Text     = $"Speed: {speed:F2} km/s";
    }
}
