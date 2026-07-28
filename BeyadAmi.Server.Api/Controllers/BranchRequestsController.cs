using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.BranchRequests;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchRequestsController : ControllerBase
    {
        private readonly IBranchRequestService _service;

        public BranchRequestsController(IBranchRequestService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all branch requests
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BranchRequestDto>), 200)]
        public async Task<ActionResult<IEnumerable<BranchRequestDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get branch request by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BranchRequestDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BranchRequestDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all branch requests for a specific branch
        /// </summary>
        [HttpGet("branch/{branchId:int}")]
        [ProducesResponseType(typeof(IEnumerable<BranchRequestDto>), 200)]
        public async Task<ActionResult<IEnumerable<BranchRequestDto>>> GetByBranch(int branchId, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByBranchAsync(branchId, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a new branch request
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateBranchRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var newId = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Update branch request completion status
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Complete(int id, [FromBody] UpdateBranchRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            await _service.CompleteAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete a branch request
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
