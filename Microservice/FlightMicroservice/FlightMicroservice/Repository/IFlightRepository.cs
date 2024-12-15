using FlightMicroservice.Models;

namespace FlightMicroservice.Repository
{
    public interface IFlightRepository
    {
        IEnumerable<Flight> GetFlights();
        Flight GetFlightByID(string FlightID);
        void InsertFlight(Flight flight);
        void DeleteFlight(string FlightID);
        void UpdateFlight(Flight flight);
        void Save();
    }
}
