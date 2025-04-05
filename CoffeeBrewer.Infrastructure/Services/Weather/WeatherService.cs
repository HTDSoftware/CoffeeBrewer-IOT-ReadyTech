using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoffeeBrewer.Infrastructure.Services.Weather
{
    /// <summary>
    /// Service for weather-related operations.
    /// </summary>
    public class WeatherService(HttpClient httpClient, IGlobalPositionLocator globalPositionLocator,
        IIPGeolocationService ipGeolocationService, IHttpContextAccessor httpContextAccessor,
        IOptions<AppSettings> appSettings) : IWeatherService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IGlobalPositionLocator _globalPositionLocator = globalPositionLocator;
        private readonly IIPGeolocationService _ipGeolocationService = ipGeolocationService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        /// <summary>
        /// Determines whether the current weather is too hot for a hot drink.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether it is too hot for a hot drink.</returns>
        public async Task<bool> TooHotForHotDrink()
        {
            // Get the API key from the configuration
            var apiKey = _appSettings.Value.ApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                return false;
            }

            LocationDetails? locationDetails = null;

            // Get the location details based on the IP address
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress) || ipAddress == "::1")
            {
                // If the IP address is not available or is localhost, use the default location from the configuration
                locationDetails ??= _appSettings.Value.DefaultLocation;
            }
            else
            {
                // Use the IP geolocation service to get the location details
                locationDetails = await _ipGeolocationService.GetLocationDetailsAsync(ipAddress);
            }

            // If we can't find location details anywhere then return false
            if (locationDetails == null)
            {
                return false;
            }

            // Get the global position based on the location details
            var globalPosition = await _globalPositionLocator.LocateGlobalPosition(locationDetails);
            if (globalPosition == null)
            {
                return false;
            }

            // Make a request to the OpenWeatherMap API to get the current weather data
            var requestUri = $"{_appSettings.Value.ApiUris.WeatherApi}?lat={globalPosition.Lat}&lon={globalPosition.Lon}&appid={apiKey}&units=metric";
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            // Read the response content and deserialize it into a WeatherData object
            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherData>(content);

            // Validate the deserialized object
            if (weatherData == null || weatherData.Main == null)
            {
                return false;
            }

            // Get the minimum temperature for iced drinks from the configuration
            var minTem4Iced = _appSettings.Value.MinmumTempForIced;
            if (minTem4Iced == 0) minTem4Iced = 30;

            // Check if the current temperature is above the minimum temperature for iced drinks
            return weatherData.Main.Temp > minTem4Iced;
        }
    }
}
