using System;

namespace WeatherApp.Services;

public class ConnectivityService : IConnectivityService
{
    IConnectivity _connectivity;

    public ConnectivityService(IConnectivity connectivity)
    {
        _connectivity = connectivity;
    }

    public bool CheckConnection()
    {
        bool connectionAvaillable = _connectivity.NetworkAccess == NetworkAccess.Internet;
        return connectionAvaillable;
    }
}

