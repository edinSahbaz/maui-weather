using Microsoft.Extensions.Logging;
using WeatherApp.View;
using WeatherApp.ViewModel;
using CommunityToolkit.Maui;
using WeatherApp.Services;
using WeatherAPI.Standard;

namespace WeatherApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
            fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
            fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
            fonts.AddFont("MaterialIcons-Regular.ttf", "IconFontTypes");
        }).UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
        builder.Services.AddSingleton<IWeatherService, WeatherService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<WeatherAPIClient>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<FavouritesPage>();
        builder.Services.AddSingleton<AboutPage>();

        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<FavouritesViewModel>();
        builder.Services.AddSingleton<AboutViewModel>();

        return builder.Build();
    }
}