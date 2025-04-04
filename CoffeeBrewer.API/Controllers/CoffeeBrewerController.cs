using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBrewer.API.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    /// <summary>
    /// Controller for handling coffee brewing requests.
    /// </summary>
    public class CoffeeController(ICoffeeMachineService coffeeService) : ControllerBase
    {
        // Service for brewing coffee
        private readonly ICoffeeMachineService _coffeeService = coffeeService;

        [HttpGet]
        /// <summary>
        /// Endpoint to brew coffee.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> representing the result of the brewing operation.</returns>
        public async Task<ActionResult> CoffeeBrewer()
        {
            // Call the BrewAsync method of the service
            var actionResult = await _coffeeService.BrewAsync();

            // Check if the result is an ObjectResult and if it has a value
            if (actionResult.Result is not ObjectResult result || result.Value == null)
            {
                return StatusCode(500, new { status = 500, message = "An unexpected error occurred." }); // Return 500 if the result is invalid
            }

            // Cast the ObjectResult to BrewResult
            var brewResult = (BrewResult)result.Value;

            // Handle the HTTP status codes based on the Status property of BrewResult
            return result.StatusCode switch
            {
                200 => Ok(new
                {
                    status = brewResult.Status,
                    message = brewResult.Message,
                    prepared = brewResult.Prepared?.ToString("o")
                }), // Return 200 OK with the brew result
                503 => StatusCode(503, new
                {
                    status = brewResult.Status
                }), // Return 503 Service Unavailable
                418 => StatusCode(418, new
                {
                    status = brewResult.Status
                }), // Return 418 I'm a teapot
                _ => StatusCode(500, new
                {
                    status = 500,
                    message = "An unexpected error occurred."
                }) // Return 500 Internal Server Error for other cases
            };
        }
    }
}