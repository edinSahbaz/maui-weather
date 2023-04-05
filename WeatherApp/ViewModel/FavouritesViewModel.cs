using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WeatherAPI.Standard.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModel;

public partial class FavouritesViewModel : BaseViewModel
{
    ILocationService _locationService;
    IWeatherService _weatherService;

    [ObservableProperty]
    private ObservableCollection<CurrentJsonResponse> favouriteLocations;

    public FavouritesViewModel(ILocationService locationService, IWeatherService weatherService)
    {
        _locationService = locationService;
        _weatherService = weatherService;

        Task.Run(PopulateFavouritesList);
    }

    async Task<string> GetCurrentLocationQuery()
    {
        var locationData = await _locationService.GetCurrentLocation();
        var currentLocation = $"{locationData.Latitude},{locationData.Longitude}";

        return currentLocation;
    }

    async Task PopulateFavouritesList()
    {
        var q = await GetCurrentLocationQuery();
        var currentLocationData = _weatherService.GetCurrentData(q);

        // TEMP CODE FOR TESTING
        var c = _weatherService.GetCurrentData("Maglaj");
        var z = _weatherService.GetCurrentData("Zenica");
        var b = _weatherService.GetCurrentData("Tuzla");
        var n = _weatherService.GetCurrentData("Cazin");
        var m = _weatherService.GetCurrentData("Mostar");

        var temp = new ObservableCollection<CurrentJsonResponse>();
        temp.Add(currentLocationData);
        temp.Add(c);
        temp.Add(z);
        temp.Add(b);
        temp.Add(n);
        temp.Add(m);

        foreach (var data in temp)
            data.Location.Localtime = data.Location.Localtime.Substring(11);

        FavouriteLocations = temp;
    }
}

