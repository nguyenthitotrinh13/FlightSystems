namespace FlightMicroservice.Models
{
    public class Flight
    {
        public string FlightID { get; set; }
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
