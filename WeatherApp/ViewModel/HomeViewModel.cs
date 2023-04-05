using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WeatherAPI.Standard;
using WeatherAPI.Standard.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    IConnectivityService _connectivityService;
    ILocationService _locationService;
    IWeatherService _weatherService;
    IAlertService _alertService;

    [ObservableProperty]
    private WeatherAPI.Standard.Models.Location currentLocation;
    [ObservableProperty]
    private Current currentWeather;
    [ObservableProperty]
    private ObservableCollection<Hour> weatherForecastHours;
    [ObservableProperty]
    private ObservableCollection<Forecastday> weatherForecastDays; 

    public HomeViewModel(IConnectivityService connectivityService, ILocationService locationService,
        IWeatherService weatherService, IAlertService alertService)
    {
        Title = "Weather App";

        _connectivityService = connectivityService;
        _locationService = locationService;
        _weatherService = weatherService;
        _alertService = alertService;

        Task.Run(LoadWeatherData);
    }

    async Task LoadWeatherData()
    {
        if (IsBusy) return;

        try
        {
            if (!_connectivityService.CheckConnection())
                throw new Exception("Please check your internet connection!");

            // Get current location
            var currentLocation = await _locationService.GetCurrentLocation();

            // Gets all weather data for location
            var q = $"{currentLocation.Latitude},{currentLocation.Longitude}";
            var forecastWeather = await _weatherService.GetAllWeatherData(q);

            // Sets current loaction and weather data
            CurrentLocation = forecastWeather.Location;
            CurrentWeather = forecastWeather.Current;

            // Sets weather data for next 24 hours
            WeatherForecastHours = SetNext24HoursData(forecastWeather);

            // Sets next 3 days forecast and modifies day property to display day name
            var forecastDays = new ObservableCollection<Forecastday>(forecastWeather.Forecast.Forecastday);
            SetDayNames(forecastDays);
            WeatherForecastDays = forecastDays;
        }
        catch (Exception e)
        {
            _alertService.DisplayAlert(Title, e.Message, "OK");
        }
    }

    static void SetDayNames(ObservableCollection<Forecastday> forecastDays)
    {
        foreach (var day in forecastDays)
        {
            var date = DateTime.Parse(day.Date);
            var dayName = date.DayOfWeek.ToString();

            day.Date = dayName == DateTime.Now.DayOfWeek.ToString() ? "Today" : dayName;
        }
    }

    static ObservableCollection<Hour> SetNext24HoursData(ForecastJsonResponse forecastWeather)
    {
        // Gets Weather of 2 days from now
        var forecastNext48Hours = new List<Hour>(forecastWeather.Forecast.Forecastday[0].Hour);
        forecastNext48Hours.AddRange(forecastWeather.Forecast.Forecastday[1].Hour);

        // Finds index of next hour
        var nextHourIndex = forecastNext48Hours.IndexOf(forecastNext48Hours.Where(x => x.Time.CompareTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm")) > 0).FirstOrDefault());

        // Modify Time so it shows correct format
        foreach (var hour in forecastNext48Hours)
            hour.Time = hour.Time.Substring(11);

        // Sets weather data 24 hours from now
        var output = new ObservableCollection<Hour>(forecastNext48Hours.GetRange(nextHourIndex, 24));

        return output;
    }
}

