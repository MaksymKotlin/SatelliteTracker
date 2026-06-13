using System;

using SGPdotNET.Observation;

namespace SatelliteTracker2D.Services;

public class IssPredictor
{
    private Satellite? _satellite;

    public void LoadTle(string[] tleLines)
    {
        _satellite = new Satellite(tleLines[0], tleLines[1], tleLines[2]);
    }

    public (double Latitude, double Longitude, double Speed) GetCurrentPosition()
    {
        if (_satellite == null)
            throw new InvalidOperationException("TLE not loaded yet.");

        var eci = _satellite.Predict(DateTime.UtcNow);
        var geo = eci.ToGeodetic();
        double speed = Math.Sqrt(
            eci.Velocity.X * eci.Velocity.X +
            eci.Velocity.Y * eci.Velocity.Y +
            eci.Velocity.Z * eci.Velocity.Z);



        return (geo.Latitude.Degrees, geo.Longitude.Degrees, speed);
    }
}
