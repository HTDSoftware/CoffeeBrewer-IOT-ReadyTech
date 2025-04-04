using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBrewer.API.Endpoints
{
    /// <summary>
    /// Provides extension methods to map coffee brewing endpoints.
    /// </summary>
    public static class CoffeeBrewerEndpoint
    {
        /// <summary>
        /// Extension method to map the coffee brewing endpoint.
        /// </summary>
        /// <param name="app">The endpoint route builder.</param>
        /// <returns>The modified endpoint route builder.</returns>
        public static IEndpointRouteBuilder MapCoffeeBrewer(this IEndpointRouteBuilder app)
        {
            // Define a GET endpoint for brewing coffee using minimal APIs
            app.MapGet("/brew-coffee-minapi", async (HttpContext context, ICoffeeMachineService service) =>
            {
                // Call the BrewAsync method of the service
                var actionResult = await service.BrewAsync();

                // Check if the result is an ObjectResult and if it has a value
                if (actionResult.Result is not ObjectResult result || result.Value == null)
                {
                    return Results.StatusCode(500); // Return 500 if the result is invalid
                }

                // Cast the ObjectResult to BrewResult
                var brewResult = (BrewResult)result.Value;

                // Handle the HTTP status codes based on the Status property of BrewResult
                return result.StatusCode switch
                {
                    200 => Results.Ok(new
                    {
                        status = brewResult.Status,
                        message = brewResult.Message,
                        prepared = brewResult.Prepared?.ToString("o")
                    }), // Return 200 OK with the brew result
                    503 => Results.StatusCode(503), // Return 503 Service Unavailable
                    418 => Results.StatusCode(418), // Return 418 I'm a teapot
                    _ => Results.StatusCode(500) // Return 500 Internal Server Error for other cases
                };
            });

            return app; // Return the modified endpoint route builder
        }
    }
}