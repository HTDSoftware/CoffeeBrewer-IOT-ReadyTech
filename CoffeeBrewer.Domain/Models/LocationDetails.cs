
namespace CoffeeBrewer.Domain.Models
{
    /// <summary>
    /// Represents the details of a location.
    /// </summary>
    public class LocationDetails
    {
        /// <summary>
        /// Gets or sets the city of the location.
        /// </summary>
        public required string City { get; set; }

        /// <summary>
        /// Gets or sets the state of the location.
        /// </summary>
        public required string State { get; set; }

        /// <summary>
        /// Gets or sets the country code of the location.
        /// </summary>
        public required string CountryCode { get; set; }

        /// <summary>
        /// Implicitly converts a string to a <see cref="LocationDetails"/> object.
        /// </summary>
        /// <param name="v">The string to convert.</param>
        /// <returns>A <see cref="LocationDetails"/> object.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
        public static implicit operator LocationDetails?(string? v)
        {
            throw new NotImplementedException();
        }
    }
}