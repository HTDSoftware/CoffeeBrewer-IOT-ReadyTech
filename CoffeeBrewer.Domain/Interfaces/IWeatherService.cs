namespace CoffeeBrewer.Domain.Interfaces
{
    /// <summary>
    /// Interface for weather-related services.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Determines whether the current weather is too hot for a hot drink.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether it is too hot for a hot drink.</returns>
        Task<bool> TooHotForHotDrink();
    }

}
