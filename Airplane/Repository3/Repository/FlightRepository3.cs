using Microsoft.EntityFrameworkCore;
using Repository3.Db;
using Repository3.Entities;

namespace Repository3.Repository
{
    public class FlightRepository3 : IFlightRepository3, IDisposable
    {
        public FlightRepository3()
        {
        }
        public void Dispose()
        {
        }

        public List<Flight> GetFlights()
        {
            using var context = new Rise2Context();
            return context.Flights.ToList();
        }

        public List<Flight> GetFlightsByFilter(string filter, string value)
        {
            //get flights with filter which is some property of flight and value of that property
            using var context = new Rise2Context();
            var result = context.Flights.Where(flight => EF.Property<string>(flight, filter) == value);


            return result.ToList(); ;
        }

        public Flight? GetLatestFlightFromDistination(string location)
        {
            using var context = new Rise2Context();
            var query = from flight in context.Flights
                        where flight.FromAirport == location
                        orderby flight.DepartureDateTime descending
                        select new Flight
                        {
                            FlightNumber = flight.FlightNumber,
                            FromAirport = flight.FromAirport,
                            ToAirport = flight.ToAirport,
                            DepartureDateTime = flight.DepartureDateTime,
                            ArrivalDateTime = flight.ArrivalDateTime
                        };
            return query.FirstOrDefault();
        }


        public Flight? GetLatestFlightFromDistinationNew(string location)
        {
            using var context = new Rise2Context();
            var query = context.Flights.Where(flight => flight.FromAirport == location)
                .OrderByDescending(flight => flight.DepartureDateTime);

            return query.FirstOrDefault();
        }

        public List<Flight> GetFlightsFromAirport(string location)
        {
            using var context = new Rise2Context();
            var query = (from airport in context.Airports
                         where airport.Name == location
                         join flight in context.Flights on airport.Code equals flight.FromAirport
                         select new Flight
                         {
                             FlightNumber = flight.FlightNumber,
                             FromAirport = flight.FromAirport,
                             ToAirport = flight.ToAirport,
                             DepartureDateTime = flight.DepartureDateTime,
                             ArrivalDateTime = flight.ArrivalDateTime
                         });
            return query.ToList();

        }

        public List<Flight> GetFlightsFromAirportNew(string location)
        {
            using var context = new Rise2Context();
            var query = context.Airports.Where(airport => airport.Name == location)
                .Join(context.Flights, airport => airport.Code, flight => flight.FromAirport, (airport, flight) => new Flight
                {
                    FlightNumber = flight.FlightNumber,
                    FromAirport = flight.FromAirport,
                    ToAirport = flight.ToAirport,
                    DepartureDateTime = flight.DepartureDateTime,
                    ArrivalDateTime = flight.ArrivalDateTime
                });
            return query.ToList();
        }

        public List<Flight> GetFlightsFromAirportNewWithInclude(string location)
        {
            using var context = new Rise2Context();
            var query = context.Airports.Where(airport => airport.Name == location)
                .Include(airport => airport.FlightFromAirportNavigations)
                .SelectMany(airport => airport.FlightFromAirportNavigations)
                .Select(flight => new Flight
                {
                    FlightNumber = flight.FlightNumber,
                    FromAirport = flight.FromAirport,
                    ToAirport = flight.ToAirport,
                    DepartureDateTime = flight.DepartureDateTime,
                    ArrivalDateTime = flight.ArrivalDateTime
                });
            return query.ToList();
        }

        public void AddFlight(Flight flight)
        {
            using var context = new Rise2Context();
            context.Flights.Add(flight);
            context.SaveChanges();
        }

        public void AddFlightToAirport(Flight flight)
        {
            using var context = new Rise2Context();
            var airport = context.Airports.FirstOrDefault(airport => airport.Code == flight.FromAirport);
            if (airport != null)
            {
                airport.FlightFromAirportNavigations.Add(flight);
                context.SaveChanges();
            }
        }

        public void UpdateFlight(Flight flight)
        {
            using var context = new Rise2Context();
            context.Flights.Update(flight);
            context.SaveChanges();
        }

        public void UpdateFlightDepartureTime(string id, DateTime time)
        {
            using var context = new Rise2Context();
            var flight = context.Flights.FirstOrDefault(flight => flight.FlightNumber == id);
            if (flight != null)
            {
                flight.DepartureDateTime = time;
                context.SaveChanges();
            }
        }

        public void DeleteFlight(string id)
        {
            using var context = new Rise2Context();
            var flight = context.Flights.FirstOrDefault(flight => flight.FlightNumber == id);
            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }
        }
    }
}
