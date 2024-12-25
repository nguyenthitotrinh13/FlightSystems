using FlightMicroservice.Models;

namespace FlightMicroservice.Repository
{
    public interface IFlightRepository
    {
        IEnumerable<Flight> GetFlights();
        Flight GetFlightByID(int FlightID);
        void InsertFlight(Flight flight);
        void DeleteFlight(int FlightID);
        void UpdateFlight(Flight flight);
        void Save();
    }
}
