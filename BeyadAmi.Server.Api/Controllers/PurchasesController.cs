using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.Purchases;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchasesController(IPurchaseService service)
        {
            _service = service;
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

            var newId = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
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
