using FlightMicroservice.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FlightMicroservice.DBContexts;
using System.Linq;
using System.IO.Compression;

namespace FlightMicroservice.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentContext _dbContext;
        public DocumentRepository(DocumentContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Document>> GetDocumentsByFilterAsync(int? flightCode, DateTime? createdDate)
        {
            var query = _dbContext.Documents.AsQueryable();

            if (flightCode.HasValue)
            {
                query = query.Where(d => d.FlightId == flightCode.Value);
            }

            if (createdDate.HasValue)
            {
                query = query.Where(d => d.CreatedDate.Date == createdDate.Value.Date);
            }

            return await query.ToListAsync();
        }

        public async Task<byte[]> CreateZipFileAsync(IEnumerable<string> filePaths)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var filePath in filePaths)
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            var fileName = Path.GetFileName(filePath);
                            var entry = archive.CreateEntry(fileName, CompressionLevel.Fastest);

                            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            using (var entryStream = entry.Open())
                            {
                                await fileStream.CopyToAsync(entryStream);
                            }
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public void DeleteDocument(int documentId)
        {
            var document = _dbContext.Documents.Find(documentId);
            _dbContext.Documents.Remove(document);
            Save();
        }
        public Document GetDocumentByID(int documentId)
        {
            return _dbContext.Documents.Find(documentId);
        }
        public async Task<List<Document>> GetDocumentsByIdsAsync(List<int> documentIds)
        {
            return await _dbContext.Documents
                .Where(d => documentIds.Contains(d.DocumentId))
                .ToListAsync();
        }

        public IEnumerable<Document> GetDocumentsByFlightCode(int flightCode)
        {
            return _dbContext.Documents.Where(d => d.FlightId == flightCode).ToList();
        }
        public IEnumerable<Document> GetDocumentsByCreatedDate(DateTime createdDate)
        {
            return _dbContext.Documents.Where(d => d.CreatedDate.Date == createdDate.Date).ToList();
        }

        public IEnumerable<Document> GetDocument()
        {
            return _dbContext.Documents.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateDocument(Document document)
        {
            _dbContext.Entry(document).State = EntityState.Modified;
            Save();
        }
        public async Task InsertDocument(IFormFile file, int flightId, DateTime createdDate)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", file.FileName);

            var fileDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var document = new Document
            {
                DocumentType = file.ContentType,
                FilePath = filePath,
                CreatedDate = createdDate,
                FlightId = flightId,
                Version = "1.0",
                ModifiedCount = 0
            };

            _dbContext.Add(document);
            await _dbContext.SaveChangesAsync();
        }
      
        public async Task UpdateDocumentAsync(int documentId, string newFilePath, string modifiedBy)
        {
            var currentDocument = _dbContext.Documents.FirstOrDefault(d => d.DocumentId == documentId);

            if (currentDocument == null)
                throw new Exception("Document not found.");

            currentDocument.ModifiedCount++;

            var currentVersion = currentDocument.Version; 
            var versionParts = currentVersion.Split('.');

            int major = int.Parse(versionParts[0]);
            int minor = currentDocument.ModifiedCount; 

            string newVersion = $"{major}.{minor}";  

            var updatedDocument = new Document
            {
                FlightId = currentDocument.FlightId,
                DocumentType = currentDocument.DocumentType,
                FilePath = newFilePath,
                CreatedDate = DateTime.Now, 
                Version = newVersion, 
                ModifiedBy = modifiedBy, 
                ModifiedDate = DateTime.Now 
            };

            _dbContext.Documents.Add(updatedDocument);
            await _dbContext.SaveChangesAsync();
        }

    }
}
