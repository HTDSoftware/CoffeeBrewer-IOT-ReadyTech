using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoffeeBrewer.Infrastructure.Services.Weather
{
    /// <summary>
    /// Service for retrieving location details based on an IP address.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="IPGeolocationService"/> class.
    /// </remarks>
    /// <param name="httpClient">The HTTP client to use for making requests.</param>
    public class IPGeolocationService(HttpClient httpClient, IOptions<AppSettings> appSettings) : IIPGeolocationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        /// <summary>
        /// Asynchronously gets the location details for the specified IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address to get location details for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the location details if found; otherwise, null.</returns>
        public async Task<LocationDetails?> GetLocationDetailsAsync(string ipAddress)
        {
            // Validate the IP address
            var requestUri = $"{_appSettings.Value.ApiUris.IpInfoApi}{ipAddress}/json";
            var response = await _httpClient.GetAsync(requestUri);

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Read the response content
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                // Deserialize the JSON response into a LocationDetails object
                var locationDetails = JsonSerializer.Deserialize<LocationDetails>(content);

                // Validate the deserialized object
                if (locationDetails == null || string.IsNullOrEmpty(locationDetails.City) ||
                    string.IsNullOrEmpty(locationDetails.State) || string.IsNullOrEmpty(locationDetails.CountryCode))
                {
                    return null;
                }

                return locationDetails;
            }
            catch (JsonException)
            {
                // Handle JSON deserialization errors
                return null;
            }
        }
    }
}
