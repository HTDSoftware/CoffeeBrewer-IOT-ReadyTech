namespace CoffeeBrewer.Domain.Models
{
    /// <summary>
    /// Represents the result of a coffee brewing operation.
    /// </summary>
    public class BrewResult
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the brewing operation.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the message describing the result of the brewing operation.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the coffee was prepared.
        /// </summary>
        public DateTimeOffset? Prepared { get; set; }
    }
}
