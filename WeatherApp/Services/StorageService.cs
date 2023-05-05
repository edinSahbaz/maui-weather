using SQLite;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class StorageService : IStorageService
{
    SQLiteAsyncConnection _db;

    async Task Init()
    {
        if (_db is not null)
            return;

        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WeatherAppData.db3");

        _db = new SQLiteAsyncConnection(dbPath);
        await _db.CreateTableAsync<StoredLocation>();
    }

    public async Task AddLocation(string cityName)
    {
        await Init();

        var location = new StoredLocation
        {
            CityName = cityName
        };

        var id = await _db.InsertAsync(location);
    }

    public async Task RemoveLocation(int id)
    {
        await Init();

        await _db.DeleteAsync<StoredLocation>(id);
    }

    public async Task<IEnumerable<StoredLocation>> GetLocations()
    {
        await Init();

        var locations = await _db.Table<StoredLocation>().ToListAsync();
        return locations;
    }
}

