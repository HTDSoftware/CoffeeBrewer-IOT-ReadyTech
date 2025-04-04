using CoffeeBrewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBrewer.Domain.Interfaces
{
    /// <summary>
    /// Interface for the coffee machine service.
    /// </summary>
    public interface ICoffeeMachineService
    {
        /// <summary>
        /// Asynchronously brews coffee and returns the result.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, with an <see cref="ActionResult{BrewResult}"/> as the result.</returns>
        Task<ActionResult<BrewResult>> BrewAsync();
    }
}
