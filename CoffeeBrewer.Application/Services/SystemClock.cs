using CoffeeBrewer.Domain.Interfaces;

namespace CoffeeBrewer.Application.Services
{
    /// <summary>
    /// Provides the current system time.
    /// </summary>
    public class SystemClock : IClock
    {
        /// <summary>
        /// Gets the current date and time.
        /// </summary>
        /// <returns>The current <see cref="DateTimeOffset"/>.</returns>
        public DateTimeOffset Now() => DateTimeOffset.Now;
    }
}