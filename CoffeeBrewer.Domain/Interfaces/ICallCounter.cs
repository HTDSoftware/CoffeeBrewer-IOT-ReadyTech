namespace CoffeeBrewer.Domain.Interfaces
{
    /// <summary>
    /// Interface for a call counter.
    /// </summary>
    public interface ICallCounter
    {
        /// <summary>
        /// Asynchronously increments the call count and returns the new value.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, with the new call count as the result.</returns>
        Task<int> IncrementAndGetAsync();
    }
}
