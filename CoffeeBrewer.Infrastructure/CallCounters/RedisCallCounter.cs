using CoffeeBrewer.Domain.Interfaces;
using StackExchange.Redis;

namespace CoffeeBrewer.Infrastructure.CallCounters
{
    /// <summary>
    /// Implementation of <see cref="ICallCounter"/> using Redis.
    /// </summary>
    public class RedisCallCounter(IConnectionMultiplexer redis) : ICallCounter
    {
        // Redis database instance
        private readonly IDatabase _db = redis.GetDatabase();

        // Key used for storing the call count in Redis
        private const string Key = "brew-coffee-counter";

        /// <summary>
        /// Asynchronously increments the call count and returns the new value.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, with the new call count as the result.</returns>
        public async Task<int> IncrementAndGetAsync()
        {
            var result = await _db.StringIncrementAsync(Key); // Increment the value in Redis
            return (int)result; // Return the incremented value as an integer
        }
    }
}
