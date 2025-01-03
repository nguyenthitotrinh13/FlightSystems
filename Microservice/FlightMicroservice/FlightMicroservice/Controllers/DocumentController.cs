using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using FlightMicroservice.Repository;
using FlightMicroservice.Models;

namespace FlightMicroservice.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        [HttpGet("filter/flightcode")]
        public ActionResult<IEnumerable<Document>> GetDocumentsByFlightCode([FromQuery] int flightCode)
        {
            var documents = _documentRepository.GetDocumentsByFlightCode(flightCode);

            if (!documents.Any())
            {
                return NotFound("No documents found for this flight code.");
            }

            return Ok(documents);
        }

        [HttpGet("filter/createddate")]
        public ActionResult<IEnumerable<Document>> GetDocumentsByCreatedDate([FromForm] DateTime createdDate)
        {
            var documents = _documentRepository.GetDocumentsByCreatedDate(createdDate);

            if (!documents.Any())
            {
                return NotFound("No documents found for the given creation date.");
            }

            return Ok(documents);
        }
        [HttpGet("export-all")]
        public async Task<IActionResult> ExportAllDocuments([FromQuery] int? flightCode, [FromQuery] DateTime? createdDate)
        {
            var documents = await _documentRepository.GetDocumentsByFilterAsync(flightCode, createdDate);

            if (!documents.Any())
            {
                return NotFound("No documents found for the given filters.");
            }

            var filePaths = documents.Select(d => d.FilePath).ToList();
            var zipFile = await _documentRepository.CreateZipFileAsync(filePaths);

            return File(zipFile, "application/zip", "FilteredDocuments.zip");
        }

        [HttpPost("export-selected")]
        public async Task<IActionResult> ExportSelectedDocuments([FromBody] List<int> documentIds)
        {
            var documents = await _documentRepository.GetDocumentsByIdsAsync(documentIds);

            if (!documents.Any())
            {
                return NotFound("No documents found for the given IDs.");
            }

            var filePaths = documents.Select(d => d.FilePath).ToList();
            var zipFile = await _documentRepository.CreateZipFileAsync(filePaths);

            return File(zipFile, "application/zip", "SelectedDocuments.zip");
        }

        [HttpPost("upload")]
        [Authorize(Policy = "EditDocuments")]
        public async Task<IActionResult> UploadDocument(IFormFile file, [FromForm] int flightId, [FromForm] DateTime createdDate)
            {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            await _documentRepository.InsertDocument(file, flightId, createdDate);

            return Ok("File uploaded successfully.");
        }

        [HttpGet("view/{documentId}")]
        public IActionResult ViewDetailDocument(int documentId)
        {
            var document = _documentRepository.GetDocumentByID(documentId);

            if (document == null)
            {
                return NotFound("Document not found.");
            }

            var filePath = document.FilePath;
            var fileType = document.DocumentType;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, fileType);
        }
       

        [HttpGet]
        [Authorize(Policy = "ViewDocuments")]
        public IActionResult Get()
        {
            var documents = _documentRepository.GetDocument();
            return new OkObjectResult(documents);
        }

        [HttpGet("{id}", Name = "GetDocument")]
        [Authorize(Policy = "ViewDocuments")]
        public IActionResult Get(int id)
        {
            var document = _documentRepository.GetDocumentByID(id);
            if (document == null)
            {
                return NotFound();
            }
            return new OkObjectResult(document);
        }
       

        [HttpPut("edit-document")]
        public async Task<IActionResult> EditDocument([FromQuery] int documentId, [FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                var modifiedBy = User.Identity.Name;

                var filePath = Path.Combine("UploadedFiles", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Gọi repository để tạo tài liệu mới (thay vì cập nhật tài liệu cũ)
                await _documentRepository.UpdateDocumentAsync(documentId, filePath, modifiedBy);

                return Ok("Document edited successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
