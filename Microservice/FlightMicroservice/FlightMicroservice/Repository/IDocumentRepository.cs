using FlightMicroservice.Models;

namespace FlightMicroservice.Repository
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetDocument();
        Document GetDocumentByID(int DocumentId);
        Task<List<Document>> GetDocumentsByIdsAsync(List<int> documentIds);
        IEnumerable<Document> GetDocumentsByFlightCode(int flightCode);
        IEnumerable<Document> GetDocumentsByCreatedDate(DateTime createdDate);
        Task<IEnumerable<Document>> GetDocumentsByFilterAsync(int? flightCode, DateTime? createdDate);
        Task<byte[]> CreateZipFileAsync(IEnumerable<string> filePaths);
        Task UpdateDocumentAsync(int documentId, string newFilePath, string modifiedBy);
        Task InsertDocument(IFormFile file, int flightId, DateTime createdDate);
        void DeleteDocument(int DocumentId);
        void UpdateDocument(Document document);
        void Save();
    }
}