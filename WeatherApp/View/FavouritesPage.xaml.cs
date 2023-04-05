using WeatherApp.ViewModel;

namespace WeatherApp.View;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage(FavouritesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
