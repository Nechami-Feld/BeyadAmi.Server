using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.Purchases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _service;
        private readonly IWebHostEnvironment _env;

        public PurchasesController(IPurchaseService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        /// <summary>
        /// Get all purchases
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PurchaseDto>), 200)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Upload receipt file for a purchase. Stores file under wwwroot/receipts and updates purchase. Returns 200.
        /// </summary>
        [HttpPost("{id:int}/receipt")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UploadReceipt(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            // validate size (max 10 MB)
            const long maxBytes = 10 * 1024 * 1024;
            if (file.Length > maxBytes)
                return BadRequest("File is too large (max 10 MB).");

            // validate extension
            var allowed = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || Array.IndexOf(allowed, ext) < 0)
                return BadRequest("Unsupported file type. Allowed: pdf, png, jpg, jpeg.");

            // ensure directory
            var receiptsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "receipts");
            if (!Directory.Exists(receiptsFolder)) Directory.CreateDirectory(receiptsFolder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(receiptsFolder, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            // update purchase
            await _service.UpdateReceiptAsync(id, fileName, cancellationToken);

            return Ok(new { ReceiptUrl = $"/receipts/{fileName}" });
        }

        /// <summary>
        /// Get purchase by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PurchaseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PurchaseDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all purchases for a specific store
        /// </summary>
        [HttpGet("store/{storeId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PurchaseDto>), 200)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetByStore(int storeId, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByStoreAsync(storeId, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get all purchases for a specific product
        /// </summary>
        [HttpGet("product/{productId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PurchaseDto>), 200)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetByProduct(int productId, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByProductAsync(productId, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a new purchase
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreatePurchaseDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var purchase = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = purchase.PurchaseId }, purchase);
        }

        /// <summary>
        /// Update a purchase
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePurchaseDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            await _service.UpdateAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete a purchase
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
