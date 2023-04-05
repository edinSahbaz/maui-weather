using WeatherAPI.Standard.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<ForecastJsonResponse> GetAllWeatherData(string q);
    }
}