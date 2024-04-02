using Microsoft.Extensions.Configuration;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot _configuration;

        static ConfigurationManager()
        {
            // Build configuration once
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string GetConnectionString(string name)
        {
            // Get connection string from configuration
            return _configuration.GetConnectionString(name);
        }

        public static IFlightRepository GetFlightRepository()
        {

            // Get service provider
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            // Resolve repository
            var repository = serviceProvider.GetRequiredService<IFlightRepository>();

            return repository;
        }
    }
}
