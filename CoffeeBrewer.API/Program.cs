using CoffeeBrewer.API.Endpoints;
using CoffeeBrewer.Application.Services;
using CoffeeBrewer.Domain.Interfaces;
using CoffeeBrewer.Domain.Models;
using CoffeeBrewer.Infrastructure.Services.Counters;
using CoffeeBrewer.Infrastructure.Services.Weather;
using StackExchange.Redis;

namespace CoffeeBrewer.API
{
    /// <summary>
    /// The main entry point for the CoffeeBrewer application.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration files from all over the project
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>();

            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register the configuration settings
            builder.Services.Configure<AppSettings>(configuration);

            // Register IHttpContextAccessor
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register IGlobalPositionLocator
            builder.Services.AddHttpClient<IGlobalPositionLocator, GlobalPositionService>();

            // Register IPGeolocationService
            builder.Services.AddHttpClient<IIPGeolocationService, IPGeolocationService>();

            // Register WeatherService
            builder.Services.AddHttpClient<IWeatherService, WeatherService>();

            // Check if Redis should be used for call counting in which case register those services
            if (builder.Configuration.GetValue<bool>("UseRedis"))
            {
                var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
                if (string.IsNullOrEmpty(redisConnectionString))
                {
                    throw new InvalidOperationException("Redis connection string is not configured.");
                }
                builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
                builder.Services.AddSingleton<ICallCounter, RedisCallCounter>();
            }
            else
            {
                builder.Services.AddSingleton<ICallCounter, InMemoryCallCounter>();
            }

            builder.Services.AddSingleton<IClock, SystemClock>();
            builder.Services.AddSingleton<ICoffeeMachineService, CoffeeMachineService>();

            var app = builder.Build();

            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
            app.MapCoffeeBrewer();

            app.Run();
        }
    }
}