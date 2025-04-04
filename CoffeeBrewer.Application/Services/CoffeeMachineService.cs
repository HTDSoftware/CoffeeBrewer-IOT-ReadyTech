using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBrewer.Application.Services
{
    /// <summary>
    /// Service for brewing coffee.
    /// </summary>
    public class CoffeeMachineService(ICallCounter counter, IClock clock) : ICoffeeMachineService
    {
        // Counter for tracking the number of calls
        private readonly ICallCounter _counter = counter;
        // Clock for obtaining the current date and time
        private readonly IClock _clock = clock;

        /// <summary>
        /// Asynchronously brews coffee and returns the result.
        /// </summary>
        /// <returns>An <see cref="ActionResult{BrewResult}"/> representing the result of the brewing operation.</returns>
        public async Task<ActionResult<BrewResult>> BrewAsync()
        {
            var now = _clock.Now(); // Get the current date and time, which may be fixed for testing

            // Return 418 I'm a teapot on April 1st
            if (now.Month == 4 && now.Day == 1)
                return new ObjectResult(new BrewResult { Status = 418 }) { StatusCode = 418 };

            // Increment the call counter and get the new count
            int count = await _counter.IncrementAndGetAsync();

            // Return 503 Service Unavailable for every 5th call
            if (count % 5 == 0)
                return new ObjectResult(new BrewResult { Status = 503 }) { StatusCode = 503 };

            // Create a successful brew result
            var result = new BrewResult
            {
                Status = 200,
                Message = "Your piping hot coffee is ready",
                Prepared = now
            };

            // Return 200 OK with the brew result
            return new OkObjectResult(result);
        }
    }
}