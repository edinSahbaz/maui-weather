namespace WeatherApp.Services
{
    public interface ILocationService
    {
        Task<Location> GetCurrentLocation();
    }
}