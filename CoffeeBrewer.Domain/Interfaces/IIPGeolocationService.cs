using CoffeeBrewer.Domain.Models;

namespace CoffeeBrewer.Domain.Interfaces
{
    /// <summary>
    /// Interface for retrieving location details based on an IP address.
    /// </summary>
    public interface IIPGeolocationService
    {
        /// <summary>
        /// Asynchronously gets the location details for the specified IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address to get location details for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the location details if found; otherwise, null.</returns>
        Task<LocationDetails?> GetLocationDetailsAsync(string ipAddress);
    }
}
