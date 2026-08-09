using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.Products;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductDto>), 200)]
        public async Task<ActionResult<List<ProductDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var dto = await _service.GetByIdAsync(id, cancellationToken);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        /// <summary>
        /// Search products by text.
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<ProductDto>), 200)]
        public async Task<ActionResult<List<ProductDto>>> SearchAsync([FromQuery] string? text, CancellationToken cancellationToken = default)
        {
            var result = await _service.SearchAsync(text, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProductDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) return BadRequest("Request body is required.");

            var newId = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newId }, null);
        }

        /// <summary>
        /// Update an existing product.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) return BadRequest("Request body is required.");

            await _service.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
