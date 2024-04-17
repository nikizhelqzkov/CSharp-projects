using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models.ExternalConnectors;
using Repository.Configuration;
using Repository.Entities;
using Repository.Repository;
using Repository3.Repository;


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

        public static void PrintFLightWithUsing()
        {
            using var flightRepository = new FlightRepository3();
            var flightList = flightRepository.GetFlights();
            foreach (var flight in flightList)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}, Departure: {flight.FromAirport}, Destination: {flight.ToAirport}, Departure Time: {flight.DepartureDateTime}, Arrival Time: {flight.ArrivalDateTime}");
            }
        }

        public static void PrintFlightsWithFilterAndValue(string filter, string value) 
        { 
            using var flightRepository = new FlightRepository3(); 
            var flightList = flightRepository.GetFlightsByFilter(filter, value); 
            foreach (var flight in flightList)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}, Departure: {flight.FromAirport}, Destination: {flight.ToAirport}, Departure Time: {flight.DepartureDateTime}, Arrival Time: {flight.ArrivalDateTime}"); 
            }
        }

        public static void Main(string[] args)
        {
            //PrintFlights();
            PrintFLightWithUsing();
            //PrintFlightsWithFilterAndValue("FromAirport", "JFK");

        }
    }
}

