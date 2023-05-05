using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IStorageService
    {
        Task AddLocation(string cityName);
        Task<IEnumerable<StoredLocation>> GetLocations();
        Task RemoveLocation(int id);
    }
}