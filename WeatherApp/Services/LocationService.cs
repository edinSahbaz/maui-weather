using System;

namespace WeatherApp.Services;

public class LocationService : ILocationService
{
    IGeolocation _geolocation;
    IAlertService _alertService;

    public LocationService(IGeolocation geolocation, IAlertService alertService)
    {
        _geolocation = geolocation;
        _alertService = alertService;
    }

    public async Task<Location> GetCurrentLocation()
    {
        var defaultLoaction = new Location();

        try
        {
            var permission = await CheckLocationPermission();
            if (!permission) return defaultLoaction;

            var location = await Geolocation.Default.GetLastKnownLocationAsync();
            if (location == null) return defaultLoaction;

            return location;
        }
        catch (Exception e)
        {
            _alertService.DisplayAlert("Weather App", e.Message, "OK");
            return defaultLoaction;
        }
    }

    public async Task<string> GetCurrentLocationQuery()
    {
        var locationData = await GetCurrentLocation();
        var currentLocation = $"{locationData.Latitude},{locationData.Longitude}";

        return currentLocation;
    }

    async Task<bool> CheckLocationPermission()
    {
        var locationWhenInUse = new Permissions.LocationWhenInUse();
        var status = await locationWhenInUse.CheckStatusAsync();

        if (status == PermissionStatus.Granted) return true;

        var userChoice = await MainThread.InvokeOnMainThreadAsync<PermissionStatus>(async () =>
        {
            var response = await locationWhenInUse.RequestAsync();
            return response;
        });

        return userChoice == PermissionStatus.Granted;
    }
}

