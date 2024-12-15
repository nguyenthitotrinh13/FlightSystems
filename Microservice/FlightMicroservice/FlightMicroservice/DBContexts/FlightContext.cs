using Microsoft.EntityFrameworkCore;
using FlightMicroservice.Models;

namespace FlightMicroservice.DBContexts
{
    public class FlightContext: DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightID = "FL001",
                    FlightNumber = "UA123",
                    Destination = "New York",
                    DepartureTime = DateTime.Now.AddHours(2)
                },
                new Flight
                {
                    FlightID = "FL002",
                    FlightNumber = "DL456",
                    Destination = "Los Angeles",
                    DepartureTime = DateTime.Now.AddHours(3)
                },
                new Flight
                {
                    FlightID = "FL003",
                    FlightNumber = "AA789",
                    Destination = "Chicago",
                    DepartureTime = DateTime.Now.AddHours(4)
                }
            );
        }
    }
}
