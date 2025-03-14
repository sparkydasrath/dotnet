// See https://aka.ms/new-console-template for more information


namespace WeatherApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // open console and ask for location
        Console.WriteLine("Please enter a location:");

        // Read the text entered by the user
        string location = Console.ReadLine();


        // If no location is provided, use a default location
        if (string.IsNullOrEmpty(location)) location = "New York";

        string weatherData = await GetWeather.GetWeatherAsync(location);
        Console.WriteLine(weatherData);
    }
}
