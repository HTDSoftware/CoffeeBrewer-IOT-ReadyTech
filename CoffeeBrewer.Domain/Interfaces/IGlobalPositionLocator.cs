using CoffeeBrewer.Domain.Models;

namespace CoffeeBrewer.Domain.Interfaces
{
    /// <summary>
    /// Interface for locating the global position based on location details.
    /// </summary>
    public interface IGlobalPositionLocator
    {
        /// <summary>
        /// Locates the global position based on the provided location details.
        /// </summary>
        /// <param name="locationDetails">The details of the location to find the global position for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the global position if found; otherwise, null.</returns>
        Task<GlobalPosition?> LocateGlobalPosition(LocationDetails locationDetails);
    }
}
