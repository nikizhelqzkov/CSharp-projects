using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Repository;

namespace Repository.Configuration
{
    public class ServiceProviderFactory
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider GetServiceProvider()
        {
            if (_serviceProvider == null)
            {
                // Create service collection
                var services = new ServiceCollection();

                // Add services
                services.AddScoped<IFlightRepository, FlightRepository>(); // Add repository as a scoped service

                services.AddDbContext<Rise2Context>(options =>
                    options.UseSqlServer(ConfigurationManager.GetConnectionString("Local")));

                // Build service provider
                _serviceProvider = services.BuildServiceProvider();
            }

            return _serviceProvider;
        }
    }

}
