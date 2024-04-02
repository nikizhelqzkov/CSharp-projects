using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly Rise2Context _context;

        public FlightRepository(Rise2Context context)
        {
            _context = context;
        }
        public async Task<List<Flight>> GetFlights()
        {
            var response = await _context.Flights.ToListAsync();
            return response;
        }
    }
}
