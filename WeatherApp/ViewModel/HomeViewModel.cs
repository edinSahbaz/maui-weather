using System;

namespace WeatherApp.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    IConnectivity connectivity;

    public HomeViewModel(IConnectivity connectivity)
    {
        this.connectivity = connectivity;
    }

    bool CheckConnectivity()
    {
        return connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}

