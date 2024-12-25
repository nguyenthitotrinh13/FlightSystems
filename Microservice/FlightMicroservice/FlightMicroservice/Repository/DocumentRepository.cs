using FlightMicroservice.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FlightMicroservice.DBContexts;
using System.Linq;

namespace FlightMicroservice.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentContext _dbContext;
        public DocumentRepository(DocumentContext dbContext)
        {
            _dbContext = dbContext;
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

        public IEnumerable<Document> GetDocument()
        {
            return _dbContext.Documents.ToList();
        }

        public void InsertDocument(Document document)
        {
            _dbContext.Add(document);
            Save();
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
    }
}
