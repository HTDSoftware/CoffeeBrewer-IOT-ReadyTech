using CoffeeBrewer.Application.Services;
using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CoffeeBrewer.Test.Unit
{
    /// <summary>
    /// Unit tests for the CoffeeMachineService class.
    /// </summary>
    public class CoffeeMachineServiceTests
    {
        /// <summary>
        /// Helper method to brew coffee and extract the result.
        /// </summary>
        /// <param name="counter">The call counter mock.</param>
        /// <param name="clock">The clock mock.</param>
        /// <returns>The result of the brewing operation.</returns>
        private static async Task<BrewResult> BrewAndExtractResult(ICallCounter counter, IClock clock)
        {
            var service = new CoffeeMachineService(counter, clock);
            var actionResult = await service.BrewAsync();
            Assert.IsType<ActionResult<BrewResult>>(actionResult);

            var result = actionResult.Result as ObjectResult;
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            return (BrewResult)result.Value;
        }

        /// <summary>
        /// Tests that the service returns 418 on April Fools' Day.
        /// </summary>
        [Fact]
        public async Task Returns418_OnAprilFoolsDay()
        {
            var counter = Substitute.For<ICallCounter>();
            var clock = new FixedClock(4, 1);

            var brewResult = await BrewAndExtractResult(counter, clock);

            Assert.Equal(418, brewResult.Status);
        }

        /// <summary>
        /// Tests that the service returns 503 on every 5th call.
        /// </summary>
        /// <param name="count">The call count.</param>
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public async Task Returns503_Every5thCall(int count)
        {
            var counter = Substitute.For<ICallCounter>();
            counter.IncrementAndGetAsync().Returns(count);
            var clock = new FixedClock(3, 10);

            var brewResult = await BrewAndExtractResult(counter, clock);

            Assert.Equal(503, brewResult.Status);
        }

        /// <summary>
        /// Tests that the service returns 200 with a message and timestamp.
        /// </summary>
        [Fact]
        public async Task Returns200_WithMessage_AndTimestamp()
        {
            var counter = Substitute.For<ICallCounter>();
            counter.IncrementAndGetAsync().Returns(1);
            var clock = new FixedClock(3, 10);

            var brewResult = await BrewAndExtractResult(counter, clock);

            Assert.Equal(200, brewResult.Status);
            Assert.Equal("Your piping hot coffee is ready", brewResult.Message);
            Assert.NotNull(brewResult.Prepared);
        }
    }
}