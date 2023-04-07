namespace WeatherApp.Services
{
    public interface ILocationService
    {
        Task<Location> GetCurrentLocation();
        Task<string> GetCurrentLocationQuery();
    }
}