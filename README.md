# <img src="https://github.com/edinSahbaz/maui-weather/assets/47791892/97b74a60-96f9-42dc-bfa3-2b8d69b0c461" height="50px" /> MAUI Weather App

This repository contains the source code for a weather app that displays the current weather and 3-day forecast for selected or current location. The app is built using .NET MAUI, a cross-platform UI framework for building native apps for iOS, Android, macOS, Windows, and more with C# and XAML.

## Installation
To install and run the app, follow these steps:
1. Clone the repository to your local machine:
``` 
git clone https://github.com/edinSahbaz/maui-weather.git
```
2. Install the .NET MAUI SDK. You can follow the instructions in the [official documentation](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation).
3. Open the .sln solution file in Visual Studio or Visual Studio Code.
4. Run the app using the desired platform-specific tooling, such as Visual Studio's iOS or Android simulator.

## Usage
When the app is started, it will display the current weather conditions for your current location, including the temperature, humidity, wind speed, and a brief description of the weather (e.g. "Clear", "Cloudy", "Rainy"). 

The app also includes a 3-day forecast, which can be accessed below the current weather data on the main screen, and the favorites page that allows users to set up to 8 favorite locations for quick access.

## Technologies

Technologies, tools, and languages used in this project.

### Language
![Csharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

### Database
![Sqlite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)

### Tools
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)

## Arhitecture and SOLID

### Architectural Pattern
The Maui Weather App is built using the `Model-View-ViewModel-Services (MVVMS)` architectural pattern and follows the SOLID principles. Here's how:

#### 1. MVVM

MVVM: The app's architecture follows the MVVM pattern, which separates the user interface (View) from the application logic (ViewModel) and the data (Model).

The View is defined using XAML markup language in the project's Views folder. This project contains pages such as HomePage, FavouritesPage, and AboutPage.

The ViewModel is defined in the  project's ViewModel folder. This project contains the `HomeViewModel`, `FavouritesViewModel` and `AboutViewModel` classes, which provide data and behavior to the corresponding views. The BaseViewModel class provides a base implementation of the INotifyPropertyChanged interface, which allows the ViewModel to notify the View when the data changes. This is implemented via CommunityToolkit MVVM NuGet package.

Models are mainly defined in the WeatherAPI.Standard project's Models folder. This project contains classes which represent the application's data entities. The MAUI project contains model for SQLite DB.

#### 2. Services
Services: The MVVMS pattern adds a new layer to the architecture, called Services, which provides a way to decouple the application logic from the data access and other external dependencies.

The Services layer is defined in the MAUI project project, which contains interfaces and classes for data access and external service integration. For example, the ILocationService interface defines method for retrieving user's location, and the IWeatherService interface defines methods for retrieving weather information from an external service.

### SOLID principles

The app follows the SOLID principles, which are a set of guidelines for writing maintainable and extensible software.

`Single Responsibility Principle (SRP)`: Each class in the app has a single responsibility, and there is a clear separation of concerns between the View, ViewModel, and Model. For example, the FavouritesViewModel is responsible for retrieving and managing the user's favorite locations, while the HomeViewModel is responsible for retrieving and displaying weather information for a specific location.

`Open/Closed Principle (OCP)`: The app's architecture is designed to be open for extension but closed for modification. For example, if a new weather API needs to be integrated into the app, a new implementation of the IWeatherService interface can be added without modifying the existing code.

`Liskov Substitution Principle (LSP)`: The app uses interfaces to abstract the implementation details of the data access and weather services, allowing for easy substitution of alternate implementations without affecting the rest of the app.

`Interface Segregation Principle (ISP)`: The app's interfaces are designed to be small and specific to the needs of each consumer. For example, the IConnectivityService interface only defines methods for checking internet connection.

`Dependency Inversion Principle (DIP)`: The app's dependencies are inverted so that higher-level modules depend on abstractions rather than concrete implementations. For example, the FavouritesViewModel depends on the IStorageService interface to retrieve and manage the user's favorite locations, rather than depending on the StorageService class directly.

By following the MVVMS pattern and SOLID principles, the Maui Weather app is designed to be maintainable, extensible, and testable.

## Contributing
Contributions to this project are welcome! If you'd like to contribute, please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
```
git checkout -b my-feature-branch
```
3. Make your changes and commit them.
```
git add .
git commit -m "my commit message"
```
4. Push your changes to your fork.
```
git push origin my-feature-branch
```
5. Open a pull request from your fork to the main repository.

## Acknowledgments
The weather data for this app is provided by the [Weather API](https://www.weatherapi.com/).
