using CommunityToolkit.Mvvm.Input;

namespace WeatherApp.ViewModel;

public partial class AboutViewModel : BaseViewModel
{
    [RelayCommand]
    async void OpenLink()
    {
        var url = "https://www.weatherapi.com/";
        await Browser.OpenAsync(url);
    }
}

