using FlightMicroservice.Models;

namespace FlightMicroservice.Repository
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetDocument();
        Document GetDocumentByID(int DocumentId);
        void InsertDocument(Document document);
        void DeleteDocument(int DocumentId);
        void UpdateDocument(Document document);
        void Save();
    }
}