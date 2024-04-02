using Repository.Entities;

namespace Repository.Repository
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlights();
    }
}
