using System;
using WeatherAPI.Standard;
using WeatherAPI.Standard.Models;

namespace WeatherApp.Services;

public class WeatherService : IWeatherService
{
    private WeatherAPIClient _weatherAPIClient;

    public WeatherService(WeatherAPIClient weatherAPIClient)
    {
        _weatherAPIClient = weatherAPIClient;
    }

    public async Task<ForecastJsonResponse> GetAllWeatherData(string city)
    {
        var forecastWeather = await _weatherAPIClient.APIs.GetForecastWeatherAsync(city, 3);
        return forecastWeather;
    }
}

