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

    public async Task<ForecastJsonResponse> GetAllWeatherData(string q)
    {
        var forecastWeather = await _weatherAPIClient.APIs.GetForecastWeatherAsync(q, 3);
        return forecastWeather;
    }

    public CurrentJsonResponse GetCurrentData(string q)
    {
        var forecastWeather = _weatherAPIClient.APIs.GetRealtimeWeather(q);
        return forecastWeather;
    }
}

