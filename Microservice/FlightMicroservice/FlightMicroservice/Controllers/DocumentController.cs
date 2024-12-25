using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using FlightMicroservice.Repository;
using FlightMicroservice.Models;

namespace FlightMicroservice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var documents = _documentRepository.GetDocument();
            return new OkObjectResult(documents);
        }

        [HttpGet("{id}", Name = "GetDocument")]
        public IActionResult Get(int id)
        {
            var document = _documentRepository.GetDocumentByID(id);
            if (document == null)
            {
                return NotFound();
            }
            return new OkObjectResult(document);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest("Document is null.");
            }

            using (var scope = new TransactionScope())
            {
                _documentRepository.InsertDocument(document);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = document.DocumentId }, document);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest("Document is null.");
            }

            using (var scope = new TransactionScope())
            {
                _documentRepository.UpdateDocument(document);
                scope.Complete();
                return new OkResult();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var document = _documentRepository.GetDocumentByID(id);
            if (document == null)
            {
                return NotFound();
            }

            _documentRepository.DeleteDocument(id);
            return new OkResult();
        }
    }
}
