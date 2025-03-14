using System.Text.Json.Nodes;

namespace WeatherApp;

// Get API key from https://openweathermap.org/api
// using System.Text.Json;

// Create a record type to store the `temp`, `temp_min`, and `temp_max` values from the JSON response
public record WeatherData(float Temp, float TempMin, float TempMax);

public class GetWeather
{
    private const string ApiKey = "fc43e771b8bc6719b6d4518866155fe1";

    
    // Use the OpenWeatherMap API to get the weather for a given location
    public static async Task<string> GetWeatherAsync(string location)
    {
        // Create the URL for the API request
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={ApiKey}&units=imperial";

        // Create a new HttpClient
        using HttpClient client = new();

        // Send the request and get the response
        HttpResponseMessage response = await client.GetAsync(url);

        // If the response is successful, read the content and return it
        if (!response.IsSuccessStatusCode) return "Error: Unable to get weather data.";
        string content = await response.Content.ReadAsStringAsync();

        // create a simple object to store the `temp`, `temp_min` and `temp_max` values from the JSON response
        JsonNode? json = JsonNode.Parse(content);
        WeatherData weatherData = new(
            json["main"]["temp"].GetValue<float>(),
            json["main"]["temp_min"].GetValue<float>(),
            json["main"]["temp_max"].GetValue<float>()
        );

        // Return the weather data as a string
        content =
            $"Current temperature: {weatherData.Temp}°F\nMin temperature: {weatherData.TempMin}°F\nMax temperature: {weatherData.TempMax}°F";

        return content;
    }
}