using Microsoft.Extensions.Logging;
using WeatherApp.Service;
using WeatherApp.View;
using WeatherApp.ViewModel;
using CommunityToolkit.Maui;

namespace WeatherApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
            fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
            fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
            fonts.AddFont("FontAwesome-Regular-400.otf", "FaRegular");
            fonts.AddFont("FontAwesome-Solid-900.otf", "FaSolid");
        }).UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);
        builder.Services.AddSingleton<WeatherService>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomeViewModel>();
        return builder.Build();
    }
}