using Microsoft.EntityFrameworkCore;
using FlightMicroservice.Models;

namespace FlightMicroservice.DBContexts
{
    public class FlightContext : DbContext
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
                    FlightID = 1,
                    FlightNumber = "UA123",  // Cung cấp dữ liệu cho tất cả các trường bắt buộc
                    Destination = "New York",
                    DepartureTime = new DateTime(2024, 12, 18, 10, 0, 0)
                },
                new Flight
                {
                    FlightID = 2,
                    FlightNumber = "DL456",  // Cung cấp dữ liệu cho tất cả các trường bắt buộc
                    Destination = "Los Angeles",
                    DepartureTime = new DateTime(2024, 12, 18, 11, 0, 0)
                },
                new Flight
                {
                    FlightID = 3,
                    FlightNumber = "AA789",  // Cung cấp dữ liệu cho tất cả các trường bắt buộc
                    Destination = "Chicago",
                    DepartureTime = new DateTime(2024, 12, 18, 12, 0, 0)
                }
            );
        }
    }
}
