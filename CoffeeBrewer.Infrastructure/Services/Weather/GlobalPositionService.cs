using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoffeeBrewer.Infrastructure.Services.Weather
{
    /// <summary>
    /// Service for locating the global position based on location details.
    /// </summary>
    public class GlobalPositionService(HttpClient httpClient, IOptions<AppSettings> appSettings) : IGlobalPositionLocator
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        /// <summary>
        /// Locates the global position based on the provided location details.
        /// </summary>
        /// <param name="locationDetails">The details of the location to find the global position for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the global position if found; otherwise, null.</returns>
        public async Task<GlobalPosition?> LocateGlobalPosition(LocationDetails locationDetails)
        {
            // Get the API key from the configuration
            var apiKey = _appSettings.Value.ApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                return null;
            }

            // Construct the request URI using the location details
            var requestUri = $"{_appSettings.Value.ApiUris.GeolocationApi}?q={locationDetails.City},{locationDetails.State},{locationDetails.CountryCode}&limit=1&appid={apiKey}";
            var response = await _httpClient.GetAsync(requestUri);

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Read the response content and deserialize it into a list of GlobalPosition objects
            var content = await response.Content.ReadAsStringAsync();
            var positions = JsonSerializer.Deserialize<List<GlobalPosition>>(content);

            // Validate the deserialized object
            return positions?.FirstOrDefault();
        }
    }
}

