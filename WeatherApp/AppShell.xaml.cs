using WeatherApp.View;

namespace WeatherApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(FavouritesPage), typeof(FavouritesPage));
		Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
	}
}

