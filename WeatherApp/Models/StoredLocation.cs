using SQLite;

namespace WeatherApp.Models;

public class StoredLocation
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public string CityName { get; set; }
}

