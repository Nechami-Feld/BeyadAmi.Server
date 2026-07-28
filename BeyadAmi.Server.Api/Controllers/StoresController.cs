using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.DTOs.Stores;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<StoresController> _logger;

        public StoresController(IStoreService storeService, ILogger<StoresController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        /// <summary>
        /// Get all stores
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var all = await _storeService.GetAllAsync(cancellationToken);
            return Ok(all);
        }

        /// <summary>
        /// Get store by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StoreDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StoreDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var store = await _storeService.GetByIdAsync(id, cancellationToken);
            if (store == null)
                return NotFound();
            return Ok(store);
        }

        /// <summary>
        /// Create a new store
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Create([FromBody] CreateStoreDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var newId = await _storeService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Update existing store
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateStoreDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            await _storeService.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Delete a store
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _storeService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
