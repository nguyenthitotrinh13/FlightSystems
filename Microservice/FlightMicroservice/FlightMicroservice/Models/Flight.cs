using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightMicroservice.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
