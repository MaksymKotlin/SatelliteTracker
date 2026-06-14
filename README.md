# SatelliteTracker

> **SatelliteTracker** -  is a real-time ISS tracking application that shows the current position of the International Space Station on a 2D world map. It also lets you predict where the station will be at any moment in the future.

<img width="1000" alt="SatelliteTracker" src="https://github.com/user-attachments/assets/4b834c40-77cc-428b-a772-d90228466d67" />


## Features
- Real-time ISS tracking updated every second
- Predict where the ISS will be at any point in the future
- 2D world map visualization
- Displays current longitude, latitude and speed

## Tech Stack

| Area | Technology | Version |
|---|---|---|
| Language | **[C#](https://dotnet.microsoft.com/en-us/languages/csharp)** | .NET 10 |
| IDE | **[JetBrains Rider](https://www.jetbrains.com/rider/)** | 2026.1.0.1 |
| UI | **[Avalonia UI](https://avaloniaui.net/)** | 1.11.0 |
| Architecture | **[MVVM](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm)** | - |
| SGP4 predict | **[SGP.NET](https://github.com/parzivail/SGP.NET)** | 1.5.0 |


## Getting Started
**Download and Run**

1. Go to the **Releases** page and download the latest version for your OS
     - `SatelliteTracker2D-windows.zip` for Windows
     - `SatelliteTracker2D-linux.zip` for Linux
2. Extract the archive and run SatelliteTracker2D
> ⚠️ Windows users: Windows Defender or your antivirus may flag the app as unknown. This is a false positive - the app is safe. To fix it, add the executable to your antivirus exclusions.

##
**Build from Source**

Requirements: `.NET 10 SDK`
```bash
git clone https://github.com/MaksymKotlin/SatelliteTracker.git
cd SatelliteTracker2D
dotnet run
```
For macOS or if you want to explore the code and make changes - this is the way to go.

## How it works

When the app launches, it downloads the latest ISS orbital data (TLE) from the internet. During this time, the coordinates will show -- and the ISS icon will appear in the top-left corner - this is normal, just wait a few seconds.
<img width="570" height="182" alt="screenshot_20260614_182642" src="https://github.com/user-attachments/assets/a919f2de-137f-4a30-896a-13b9042b37a6" />

Once the data is loaded, the SGP4 model calculates the ISS position and updates it every second, so you always see where the station is right now.

Want to see where the ISS will be in the future? Enter the number of minutes in the input field and press Calculate. The app will predict the future position and keep it updated in real time. To return back to live tracking, press Back to Live.

<img width="434" height="176" alt="screenshot_20260614_182754" src="https://github.com/user-attachments/assets/224dd67f-78b8-42c4-9f80-ce174f53db51" />

## License
This project is released under the [Unlicense](https://unlicense.org/) - do whatever you want with the code, no attribution required.

