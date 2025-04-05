using CoffeeBrewer.Domain.Interfaces;

namespace CoffeeBrewer.Infrastructure.Services.Counters
{
    /// <summary>
    /// Implementation of <see cref="ICallCounter"/> using in-memory storage.
    /// </summary>
    public class InMemoryCallCounter : ICallCounter
    {
        // In-memory counter
        private int _counter = 0;

        /// <summary>
        /// Asynchronously increments the call count and returns the new value.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, with the new call count as the result.</returns>
        public Task<int> IncrementAndGetAsync()
        {
            _counter++; // Increment the counter
            return Task.FromResult(_counter); // Return the incremented value as a completed task
        }
    }
}