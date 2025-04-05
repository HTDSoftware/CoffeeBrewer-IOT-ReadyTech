using CoffeeBrewer.Application.Services;
using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Net;

namespace CoffeeBrewer.Test.Integration
{
    /// <summary>
    /// Integration tests for the BrewCoffee endpoint.
    /// </summary>
    public class BrewCoffeeEndpointTests
    {
        /// <summary>
        /// Tests that the endpoint returns 418 (I'm a teapot) on April Fools' Day.
        /// </summary>
        [Fact]
        public async Task Returns418_OnAprilFoolsDay()
        {
            // Create a test server factory with a fixed clock set to April 1st
            var factory = new TestServerFactory(services =>
            {
                services.AddSingleton<IClock>(new FixedClock(4, 1)); // April Fools
                var callCounter = Substitute.For<ICallCounter>();
                callCounter.IncrementAndGetAsync().Returns(1);
                services.AddSingleton(callCounter);

                // Simulate that the weather service is not too hot for a hot drink
                var weatherService = Substitute.For<IWeatherService>();
                weatherService.TooHotForHotDrink().Returns(false);
                services.AddSingleton(weatherService);
            });
            var client = factory.CreateClient();

            // Send a request to the /brew-coffee endpoint
            var response = await client.GetAsync("/brew-coffee");

            // Assert that the response status code is 418 (I'm a teapot)
            Assert.Equal((HttpStatusCode)418, response.StatusCode);
        }

        /// <summary>
        /// Tests that the endpoint returns 503 (Service Unavailable) on the 5th call.
        /// </summary>
        [Fact]
        public async Task Returns503_On5thCall()
        {
            // Create a test server factory with a fixed clock not set to April 1st
            var factory = new TestServerFactory(services =>
            {
                services.AddSingleton<IClock>(new FixedClock(3, 10)); // Not April Fools
                var callCounter = Substitute.For<ICallCounter>();
                callCounter.IncrementAndGetAsync().Returns(5); // Call count is 5
                services.AddSingleton(callCounter);

                // Simulate that the weather service is not too hot for a hot drink
                var weatherService = Substitute.For<IWeatherService>();
                weatherService.TooHotForHotDrink().Returns(false);
                services.AddSingleton(weatherService);
            });
            var client = factory.CreateClient();

            // Send a request to the /brew-coffee endpoint
            var response = await client.GetAsync("/brew-coffee");

            // Assert that the response status code is 503 (Service Unavailable)
            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }

        /// <summary>
        /// Tests that the endpoint returns 200 (OK) with a hot message when not on April Fools' Day 
        /// and not the 5th call and the temperature is below the threshold for hot drinks.
        /// </summary>
        [Fact]
        public async Task Returns200_WithHotMessage()
        {
            // Create a test server factory with a fixed clock not set to April 1st
            var factory = new TestServerFactory(services =>
            {
                services.AddSingleton<IClock>(new FixedClock(3, 10)); // Not April Fools
                var callCounter = Substitute.For<ICallCounter>();
                callCounter.IncrementAndGetAsync().Returns(1); // Call count is 1
                services.AddSingleton(callCounter);

                // Simulate that the weather service is not too hot for a hot drink
                var weatherService = Substitute.For<IWeatherService>();
                weatherService.TooHotForHotDrink().Returns(false);
                services.AddSingleton(weatherService);
            });
            var client = factory.CreateClient();

            // Send a request to the /brew-coffee endpoint
            var response = await client.GetAsync("/brew-coffee");
            var content = await response.Content.ReadAsStringAsync();

            // Assert that the response status code is 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Assert that the response content contains the expected message
            Assert.Contains("Your piping hot coffee is ready", content);
            Assert.Contains("prepared", content);
        }

        /// <summary>
        /// Tests that the endpoint returns 200 (OK) with an iced message when not on April Fools' Day 
        /// and not the 5th call and the temperature is above the threshold for hot drinks.
        /// </summary>
        [Fact]
        public async Task Returns200_WithIcedMessage()
        {
            // Create a test server factory with a fixed clock not set to April 1st
            var factory = new TestServerFactory(services =>
            {
                services.AddSingleton<IClock>(new FixedClock(3, 10)); // Not April Fools
                var callCounter = Substitute.For<ICallCounter>();
                callCounter.IncrementAndGetAsync().Returns(1); // Call count is 1
                services.AddSingleton(callCounter);

                // Simulate that the weather service is too hot for a hot drink
                var weatherService = Substitute.For<IWeatherService>();
                weatherService.TooHotForHotDrink().Returns(true);
                services.AddSingleton(weatherService);
            });
            var client = factory.CreateClient();

            // Send a request to the /brew-coffee endpoint
            var response = await client.GetAsync("/brew-coffee");
            var content = await response.Content.ReadAsStringAsync();

            // Assert that the response status code is 200 (OK)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Assert that the response content contains the expected message
            Assert.Contains("Your refreshing iced coffee is ready", content);
            Assert.Contains("prepared", content);
        }
    }
}