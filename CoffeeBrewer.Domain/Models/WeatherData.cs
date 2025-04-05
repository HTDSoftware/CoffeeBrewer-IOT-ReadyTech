using System.Text.Json.Serialization;

namespace CoffeeBrewer.Domain.Models
{
    /// <summary>
    /// Represents weather data.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Gets or sets the coordinates of the location.
        /// </summary>
        [JsonPropertyName("coord")]
        public required Coord Coord { get; set; }

        /// <summary>
        /// Gets or sets the weather conditions.
        /// </summary>
        [JsonPropertyName("weather")]
        public required List<Weather> Weather { get; set; }

        /// <summary>
        /// Gets or sets the base station.
        /// </summary>
        [JsonPropertyName("base")]
        public required string Base { get; set; }

        /// <summary>
        /// Gets or sets the main weather data.
        /// </summary>
        [JsonPropertyName("main")]
        public required MainData Main { get; set; }

        /// <summary>
        /// Gets or sets the visibility in meters.
        /// </summary>
        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        /// <summary>
        /// Gets or sets the wind data.
        /// </summary>
        [JsonPropertyName("wind")]
        public required Wind Wind { get; set; }

        // Uncomment and use if rain data is needed
        // /// <summary>
        // /// Gets or sets the rain data.
        // /// </summary>
        // [JsonPropertyName("rain")]
        // public required Rain Rain { get; set; }

        /// <summary>
        /// Gets or sets the cloud data.
        /// </summary>
        [JsonPropertyName("clouds")]
        public required Clouds Clouds { get; set; }

        /// <summary>
        /// Gets or sets the data calculation time in Unix format.
        /// </summary>
        [JsonPropertyName("dt")]
        public int Dt { get; set; }

        /// <summary>
        /// Gets or sets the system data.
        /// </summary>
        [JsonPropertyName("sys")]
        public required Sys Sys { get; set; }

        /// <summary>
        /// Gets or sets the timezone offset in seconds.
        /// </summary>
        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }

        /// <summary>
        /// Gets or sets the city ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        [JsonPropertyName("cod")]
        public int Cod { get; set; }
    }

    /// <summary>
    /// Represents the coordinates of a location.
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
    }

    /// <summary>
    /// Represents weather conditions.
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// Gets or sets the weather condition ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the group of weather parameters (Rain, Snow, Extreme, etc.).
        /// </summary>
        [JsonPropertyName("main")]
        public required string Main { get; set; }

        /// <summary>
        /// Gets or sets the weather condition within the group.
        /// </summary>
        [JsonPropertyName("description")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the weather icon ID.
        /// </summary>
        [JsonPropertyName("icon")]
        public required string Icon { get; set; }
    }

    /// <summary>
    /// Represents the main weather data.
    /// </summary>
    public class MainData
    {
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Gets or sets the temperature as perceived by humans.
        /// </summary>
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        /// <summary>
        /// Gets or sets the minimum temperature at the moment.
        /// </summary>
        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum temperature at the moment.
        /// </summary>
        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }

        /// <summary>
        /// Gets or sets the atmospheric pressure (on the sea level, if available).
        /// </summary>
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        /// <summary>
        /// Gets or sets the humidity percentage.
        /// </summary>
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Gets or sets the sea level atmospheric pressure.
        /// </summary>
        [JsonPropertyName("sea_level")]
        public int SeaLevel { get; set; }

        /// <summary>
        /// Gets or sets the ground level atmospheric pressure.
        /// </summary>
        [JsonPropertyName("grnd_level")]
        public int GrndLevel { get; set; }
    }

    /// <summary>
    /// Represents wind data.
    /// </summary>
    public class Wind
    {
        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets the wind direction in degrees.
        /// </summary>
        [JsonPropertyName("deg")]
        public int Deg { get; set; }

        /// <summary>
        /// Gets or sets the wind gust speed.
        /// </summary>
        [JsonPropertyName("gust")]
        public double Gust { get; set; }
    }

    // Uncomment and use if rain data is needed
    // /// <summary>
    // /// Represents rain data.
    // /// </summary>
    // public class Rain
    // {
    //     /// <summary>
    //     /// Gets or sets the rain volume for the last hour.
    //     /// </summary>
    //     [JsonPropertyName("onehour")]
    //     public double OneHour { get; set; }
    // }

    /// <summary>
    /// Represents cloud data.
    /// </summary>
    public class Clouds
    {
        /// <summary>
        /// Gets or sets the cloudiness percentage.
        /// </summary>
        [JsonPropertyName("all")]
        public int All { get; set; }
    }

    /// <summary>
    /// Represents system data.
    /// </summary>
    public class Sys
    {
        /// <summary>
        /// Gets or sets the internal parameter.
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the system ID.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [JsonPropertyName("country")]
        public required string Country { get; set; }

        /// <summary>
        /// Gets or sets the sunrise time in Unix format.
        /// </summary>
        [JsonPropertyName("sunrise")]
        public int Sunrise { get; set; }

        /// <summary>
        /// Gets or sets the sunset time in Unix format.
        /// </summary>
        [JsonPropertyName("sunset")]
        public int Sunset { get; set; }
    }
}
