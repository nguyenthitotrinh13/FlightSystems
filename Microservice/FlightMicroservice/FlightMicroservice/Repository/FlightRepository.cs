using FlightMicroservice.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FlightMicroservice.DBContexts;
using System.Linq;

namespace FlightMicroservice.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightContext _dbContext;
        public FlightRepository(FlightContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public void DeleteFlight(int flightId)
        {
            var flight = _dbContext.Flights.Find(flightId);
            _dbContext.Flights.Remove(flight);
            Save();
        }

        public Flight GetFlightByID(int flightID)
        {
            return _dbContext.Flights.Find(flightID);
        }

        public IEnumerable<Flight> GetFlights()
        {
            return _dbContext.Flights.ToList();
        }

        public void InsertFlight(Flight flight)
        {
            _dbContext.Add(flight);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateFlight(Flight flight)
        {
            _dbContext.Entry(flight).State = EntityState.Modified;
            Save();
        }
    }
}
