using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WeatherAPI.Standard.Models;
using WeatherApp.Services;
using CommunityToolkit.Mvvm.Input;

namespace WeatherApp.ViewModel;

public partial class FavouritesViewModel : BaseViewModel
{
    ILocationService _locationService;
    IWeatherService _weatherService;
    IAlertService _alertService;

    [ObservableProperty]
    private string cityNameEntryValue;
    [ObservableProperty]
    private ObservableCollection<CurrentJsonResponse> favouriteLocations;

    public FavouritesViewModel(ILocationService locationService, IWeatherService weatherService, IAlertService alertService)
    {
        Title = "Weather App";

        _locationService = locationService;
        _weatherService = weatherService;
        _alertService = alertService;

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

        var temp = new ObservableCollection<CurrentJsonResponse>();
        temp.Add(currentLocationData);

        // Parse Localtime to correct format
        foreach (var data in temp)
            data.Location.Localtime = data.Location.Localtime.Substring(11);

        FavouriteLocations = temp;
    }

    [RelayCommand]
    private void SaveCity()
    {
        var cityName = CityNameEntryValue;

        try
        {
            if(cityName == String.Empty || cityName == null) throw new Exception("Enter the city name, please!");

            // Tries to find if the city is already in favourites
            var cityData = FavouriteLocations.Where(x => x.Location.Name.ToLower() == cityName.ToLower()).FirstOrDefault();
            if (cityData != null) throw new Exception("City already in favourites!");

            // Tries to get data for entered city
            var data = _weatherService.GetCurrentData(cityName);

            // If the API call is sucessfull, the city name is valid then it is saved to local storage
            // else, Exception is throwed by the API and the city is not saved to storage
            data.Location.Localtime = data.Location.Localtime.Substring(11);
            FavouriteLocations.Add(data);


        }
        catch (Exception e)
        {
            var exceptionMsg = e.Message;

            // If Exception is from API, it means that it can not find
            // data for the entered city, so the city name is invalid
            if(e.Source == "WeatherAPI.Standard") exceptionMsg = "Invalid city name!";

            _alertService.DisplayAlert(Title, exceptionMsg, "Ok");
        }
    }
}

