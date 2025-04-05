namespace CoffeeBrewer.Domain.Models
{
    /// <summary>
    /// Represents the application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public required string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the default location details.
        /// </summary>
        public LocationDetails? DefaultLocation { get; set; }

        /// <summary>
        /// Gets or sets the minimum temperature for iced drinks.
        /// </summary>
        public int MinmumTempForIced { get; set; }

        /// <summary>
        /// Gets or sets the API URIs.
        /// </summary>
        public ApiUris ApiUris { get; set; } = new ApiUris
        {
            WeatherApi = string.Empty,
            GeolocationApi = string.Empty,
            IpInfoApi = string.Empty
        };
    }

    /// <summary>
    /// Represents the API URIs.
    /// </summary>
    public class ApiUris
    {
        /// <summary>
        /// Gets or sets the weather API URI.
        /// </summary>
        public required string WeatherApi { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation API URI.
        /// </summary>
        public required string GeolocationApi { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the IP info API URI.
        /// </summary>
        public required string IpInfoApi { get; set; } = string.Empty;
    }
}
