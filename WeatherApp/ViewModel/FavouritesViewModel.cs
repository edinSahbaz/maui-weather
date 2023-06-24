using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherAPI.Standard.Models;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.View;

namespace WeatherApp.ViewModel;

public partial class FavouritesViewModel : BaseViewModel
{
    ILocationService _locationService;
    IWeatherService _weatherService;
    IAlertService _alertService;
    IStorageService _storageService;

    [ObservableProperty]
    string cityNameEntryValue;
    [ObservableProperty]
    ObservableCollection<CurrentJsonResponse> favouriteLocations;

    public FavouritesViewModel(ILocationService locationService, IWeatherService weatherService,
        IAlertService alertService, IStorageService storageService)
    {
        Title = "RAMU Weather";

        _locationService = locationService;
        _weatherService = weatherService;
        _alertService = alertService;
        _storageService = storageService;

        FavouriteLocations = new ObservableCollection<CurrentJsonResponse>();
        MainThread.InvokeOnMainThreadAsync(PopulateFavouritesList);
    }

    async Task LoadCurrentLocationData()
    {
        var q = await _locationService.GetCurrentLocationQuery();
        var data = _weatherService.GetCurrentData(q);
        data.Location.Localtime = data.Location.Localtime.Substring(11);

        FavouriteLocations.Add(data);
    }

    async Task PopulateFavouritesList()
    {
        IsBusy = true;

        await LoadCurrentLocationData();

        var locations = await _storageService.GetLocations();

        // Get data for stored locations and parse local time to correct format
        foreach (var location in locations)
        {
            var data = _weatherService.GetCurrentData(location.CityName);
            data.Location.Localtime = data.Location.Localtime.Substring(11);

            FavouriteLocations.Add(data);
        }

        IsBusy = false;
    }

    [RelayCommand]
    async void SaveCity()
    {
        try
        {
            IsBusy = true;

            // Custom exceptions
            if (FavouriteLocations.Count >= 6) throw new Exception("You can have a maximum of 6 favourite locations!");


            var cityName = CityNameEntryValue;
            if (cityName == String.Empty || cityName == null) throw new Exception("Enter the city name, please!");

            // Tries to find if the city is already in favourites
            if(FavouriteLocations.Count > 0)
            {
                var cityData = FavouriteLocations.Where(x => x.Location.Name.ToLower() == cityName.ToLower()).FirstOrDefault();
                if (cityData != null) throw new Exception("City already in favourites!");
            }

            // Tries to get data for entered city
            var data = _weatherService.GetCurrentData(cityName);

            // If the API call is sucessfull, the city name is valid then it is saved to local storage
            // else, Exception is throwed by the API and the city is not saved to storage
            data.Location.Localtime = data.Location.Localtime.Substring(11);
            FavouriteLocations.Add(data);

            // Store city name to local DB
            await _storageService.AddLocation(data.Location.Name);
        }
        catch (Exception e)
        {
            var exceptionMsg = e.Message;

            // If Exception is from API, it means that it can not find
            // data for the entered city, so the city name is invalid
            if(e.Source == "WeatherAPI.Standard") exceptionMsg = "Invalid city name!";

            _alertService.DisplayAlert(Title, exceptionMsg, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToCityData(string q)
    {
        if (q == null) return;

        await Shell.Current.GoToAsync($"//{nameof(HomePage)}?location={q}", true);
    }

    [RelayCommand]
    async void DeleteLocation(string name)
    {
        name = name.ToLower();

        var locations = await _storageService.GetLocations();

        var selectedDbLocation = locations.Where(l => l.CityName.ToLower() == name).FirstOrDefault();

        if (selectedDbLocation is null) return;
        await _storageService.RemoveLocation(selectedDbLocation.Id);

        var selectedLocation = FavouriteLocations.Where(l => l.Location.Name.ToLower() == name).FirstOrDefault();

        if (selectedLocation is null) return;
        FavouriteLocations.RemoveAt(FavouriteLocations.IndexOf(selectedLocation));

        _alertService.DisplayAlert(Title, $"{selectedLocation.Location.Name} removed from Favourites!", "Ok");
    }
}

