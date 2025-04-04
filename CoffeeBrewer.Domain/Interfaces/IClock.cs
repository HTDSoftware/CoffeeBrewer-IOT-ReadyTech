namespace CoffeeBrewer.Domain.Interfaces
{
    /// <summary>
    /// Provides an interface for obtaining the current date and time.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets the current date and time.
        /// </summary>
        /// <returns>The current <see cref="DateTimeOffset"/>.</returns>
        DateTimeOffset Now();
    }
}
