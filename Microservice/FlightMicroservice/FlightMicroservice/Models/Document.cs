using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightMicroservice.Models
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FlightId { get; set; }
    }
}
