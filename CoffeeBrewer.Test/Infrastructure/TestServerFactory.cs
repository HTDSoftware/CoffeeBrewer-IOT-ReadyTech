using CoffeeBrewer.API;
using CoffeeBrewer.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBrewer.Test.Infrastructure
{
    /// <summary>
    /// A custom test server factory for configuring and creating test servers.
    /// </summary>
    /// <param name="configureServices">A delegate for configuring services.</param>
    public class TestServerFactory(Action<IServiceCollection> configureServices) : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _configureServices = configureServices;

        /// <summary>
        /// Configures the web host for the test server.
        /// </summary>
        /// <param name="builder">The web host builder.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Remove real implementations if needed
                services.RemoveAll<ICallCounter>();
                services.RemoveAll<IClock>();

                // Apply custom service configurations if provided
                _configureServices?.Invoke(services);
            });
        }
    }

    /// <summary>
    /// Extension methods for IServiceCollection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Removes all services of a specified type from the service collection.
        /// </summary>
        /// <typeparam name="TService">The type of service to remove.</typeparam>
        /// <param name="services">The service collection.</param>
        public static void RemoveAll<TService>(this IServiceCollection services)
        {
            var descriptors = services.Where(d => d.ServiceType == typeof(TService)).ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }
        }
    }
}
