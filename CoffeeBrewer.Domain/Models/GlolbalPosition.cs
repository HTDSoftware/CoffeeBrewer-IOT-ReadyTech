using System.Text.Json.Serialization;

namespace CoffeeBrewer.Domain.Models
{
    /// <summary>
    /// Represents the global position details.
    /// </summary>
    public class GlobalPosition
    {
        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the local names of the location.
        /// </summary>
        [JsonPropertyName("localnames")]
        public Dictionary<string, string>? LocalNames { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the location.
        /// </summary>
        [JsonPropertyName("lat")]
        public required double Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the location.
        /// </summary>
        [JsonPropertyName("lon")]
        public required double Lon { get; set; }

        /// <summary>
        /// Gets or sets the country of the location.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the state of the location.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get; set; }
    }
}