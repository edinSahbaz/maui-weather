using WeatherApp.ViewModel;

namespace WeatherApp.View;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
