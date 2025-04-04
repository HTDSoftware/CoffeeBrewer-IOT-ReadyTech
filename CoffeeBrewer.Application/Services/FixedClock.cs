using CoffeeBrewer.Domain.Interfaces;

namespace CoffeeBrewer.Application.Services
{
    /// <summary>
    /// Provides a fixed date and time for testing purposes.
    /// </summary>
    public class FixedClock(int month, int day) : IClock
    {
        // The fixed date and time
        private readonly DateTimeOffset _fixedTime = new(2025, month, day, 10, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Gets the fixed date and time.
        /// </summary>
        /// <returns>The fixed <see cref="DateTimeOffset"/>.</returns>
        public DateTimeOffset Now() => _fixedTime;
    }
}