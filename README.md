# SatelliteTracker

> **SatelliteTracker** - is a real-time ISS (International Space Station) tracking application built with Avalonia UI and .NET 10. It uses the SGP4 orbital prediction model with TLE (Two-Line Element) data to calculate and display the current position of the ISS on an interactive 2D world map, updating every second.

## Features
- Real-time ISS tracking updated every second
- SGP4 orbital prediction model via TLE data
- TLE data auto-refreshes every hour for accuracy
- 2D world map visualization

## Tech Stack

| Area | Technology | Version |
|---|---|---|
| Language | **[C#](https://dotnet.microsoft.com/en-us/languages/csharp)** | .NET 10 |
| IDE | **[JetBrains Rider](https://www.jetbrains.com/rider/)** | 2026.1.0.1 |
| UI | **[Avalonia UI](https://avaloniaui.net/)** | 1.11.0 |
| Architecture | **[MVVM](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm)** | - |
| SGP4 predict | **[SGP.NET](https://github.com/parzivail/SGP.NET)** | 1.5.0 |
