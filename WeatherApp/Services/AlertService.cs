using System;

namespace WeatherApp.Services;

public class AlertService : IAlertService
{
    public void DisplayAlert(string title, string message, string actionMessage)
    {
        Application.Current.Dispatcher.Dispatch(() =>
        {
            Application.Current.MainPage.DisplayAlert(title, message, actionMessage);
        });
    }
}

