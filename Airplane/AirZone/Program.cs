using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models.ExternalConnectors;
using Repository.Configuration;
using Repository.Entities;
using Repository.Repository;


namespace AirZone
{
    public static class Program
    {
        public static void PrintFlights()
        {
            var flightRepository = ConfigurationManager.GetFlightRepository();
            var flightList = flightRepository.GetFlights().Result;
            foreach (var flight in flightList)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}, Departure: {flight.FromAirport}, Destination: {flight.ToAirport}, Departure Time: {flight.DepartureDateTime}, Arrival Time: {flight.ArrivalDateTime}");
            }
        }
        public static void Main(string[] args)
        {
            PrintFlights();
        }
    }
}

