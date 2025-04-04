using CoffeeBrewer.API.Endpoints;
using CoffeeBrewer.Application.Services;
using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Infrastructure.CallCounters;
using StackExchange.Redis;

namespace CoffeeBrewer.API
{
    /// <summary>
    /// The main entry point for the CoffeeBrewer application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method, which is the entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(); // Add support for controllers
            builder.Services.AddEndpointsApiExplorer(); // Add support for endpoint API explorer

            builder.Services.AddSwaggerGen(); // Add support for Swagger/OpenAPI

            // Check if Redis should be used for call counting in which case register those services
            if (builder.Configuration.GetValue<bool>("UseRedis"))
            {
                var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
                if (string.IsNullOrEmpty(redisConnectionString))
                {
                    throw new InvalidOperationException("Redis connection string is not configured.");
                }
                // Register Redis connection and RedisCallCounter
                builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
                builder.Services.AddSingleton<ICallCounter, RedisCallCounter>();
            }
            else
            {
                // Register InMemoryCallCounter Service if Redis is not used
                builder.Services.AddSingleton<ICallCounter, InMemoryCallCounter>();
            }

            // Register the SystemClock Service which will be the IClock for production
            builder.Services.AddSingleton<IClock, SystemClock>();

            // Register the CoffeeMachineService with its interface
            builder.Services.AddSingleton<ICoffeeMachineService, CoffeeMachineService>();

            var app = builder.Build();

            // Test says to Design and implement an HTTP API not HTTPS
            // app.UseHttpsRedirection(); // Uncomment to use HTTPS redirection

            app.UseAuthorization(); // Add authorization middleware

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Enable Swagger in development environment
                app.UseSwaggerUI(); // Enable Swagger UI in development environment
            }

            // I have created a Controller API and a Minimal API
            // It obviously woundn't be a good idea to have both
            // but I wanted to show how to do both
            app.MapControllers(); // Map controller routes
            app.MapCoffeeBrewer(); // Map minimal API routes

            app.Run(); // Run the application
        }
    }
}