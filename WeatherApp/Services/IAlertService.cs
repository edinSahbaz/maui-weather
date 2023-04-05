namespace WeatherApp.Services
{
    public interface IAlertService
    {
        void DisplayAlert(string title, string message, string actionMessage);
    }
}