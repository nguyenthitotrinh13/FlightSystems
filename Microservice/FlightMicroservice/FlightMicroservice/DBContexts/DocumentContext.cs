using Microsoft.EntityFrameworkCore;
using FlightMicroservice.Models;

namespace FlightMicroservice.DBContexts
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    DocumentId = 1,
                    DocumentType = "PDF",
                    FilePath = "path/to/document1.pdf",
                    CreatedDate = new DateTime(2024, 12, 1),
                    FlightId = 1,
                    Version = "1.0",
                    ModifiedBy = null,
                    ModifiedDate = null, 
                    ModifiedCount = 0,
                },
                new Document
                {
                    DocumentId = 2,
                    DocumentType = "Word",
                    FilePath = "path/to/document2.docx",
                    CreatedDate = new DateTime(2024, 12, 5),
                    FlightId = 2,
                    Version = "1.0",
                    ModifiedBy = null,
                    ModifiedDate = null,
                    ModifiedCount = 0,
                },
                new Document
                {
                    DocumentId = 3,
                    DocumentType = "Image",
                    FilePath = "path/to/document3.jpg",
                    CreatedDate = new DateTime(2024, 12, 10),
                    FlightId = 3,
                    Version = "1.0",
                    ModifiedBy = null,
                    ModifiedDate = null,
                    ModifiedCount = 0,
                }
            );
        }
    }
}
