using Repository3.Entities;

namespace Repository3.Repository
{
    public interface IFlightRepository3
    {
        List<Flight> GetFlights();
        //Get Flights by filter as a parameter and value of containing filter
        List<Flight> GetFlightsByFilter(string filter, string value);

        Flight? GetLatestFlightFromDistination(string location);
        Flight? GetLatestFlightFromDistinationNew(string location);

        List<Flight> GetFlightsFromAirport(string location);
        List<Flight> GetFlightsFromAirportNew(string location);
        List<Flight> GetFlightsFromAirportNewWithInclude(string location);

        void AddFlight(Flight flight);

        void AddFlightToAirport(Flight flight);

        void UpdateFlight(Flight flight);

        void UpdateFlightDepartureTime(string id, DateTime time);

        void DeleteFlight(string id);
    }
}
