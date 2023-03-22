using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WeatherAPI.Standard;
using WeatherAPI.Standard.Models;

namespace WeatherApp.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    IConnectivity connectivity;

    [ObservableProperty]
    private WeatherAPI.Standard.Models.Location currentLocation;
    [ObservableProperty]
    private Current currentWeather;
    [ObservableProperty]
    private ObservableCollection<Hour> weatherForecastHours;
    [ObservableProperty]
    private ObservableCollection<Forecastday> weatherForecastDays; 

    public HomeViewModel(IConnectivity connectivity)
    {
        this.connectivity = connectivity;

        Task.Run(GetWeatherData);
    }

    async Task GetWeatherData()
    {
        if (IsBusy) return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Application.Current.Dispatcher.Dispatch(() => {
                    Application.Current.MainPage.DisplayAlert("Weather App", "Please check your internet connection!", "OK");
                });
                return;
            }

            WeatherAPIClient client = new WeatherAPIClient();
            var forecastWeather = await client.APIs.GetForecastWeatherAsync("Sarajevo", 3);

            CurrentWeather = forecastWeather.Current;
            CurrentLocation = forecastWeather.Location;

            var forecastDays = new ObservableCollection<Forecastday>(forecastWeather.Forecast.Forecastday);

            foreach (var day in forecastDays)
            {
                var date = DateTime.Parse(day.Date);
                var dayName = date.DayOfWeek.ToString();

                day.Date = dayName == DateTime.Now.DayOfWeek.ToString() ? "Today" : dayName;
            }

            WeatherForecastDays = forecastDays;

            // Gets Weather of 2 days from now
            var forecastNext48Hours = new List<Hour>(forecastWeather.Forecast.Forecastday[0].Hour);
            forecastNext48Hours.AddRange(forecastWeather.Forecast.Forecastday[1].Hour);

            // Finds index of next hour
            var nextHourIndex = forecastNext48Hours.IndexOf(forecastNext48Hours.Where(x => x.Time.CompareTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm")) > 0).FirstOrDefault());

            // Modify Time so it shows correct format
            foreach (var hour in forecastNext48Hours)
            {
                hour.Time = hour.Time.Substring(11);
            }

            // Sets weather data 24 hours from now
            WeatherForecastHours = new ObservableCollection<Hour>(forecastNext48Hours.GetRange(nextHourIndex, 24));
        }
        catch (Exception e)
        {
            Application.Current.Dispatcher.Dispatch(() => {
                Application.Current.MainPage.DisplayAlert("Weather App", e.Message, "OK");
            });
        }
    }
}

