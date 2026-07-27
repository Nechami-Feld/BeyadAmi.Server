using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.DTOs.Branches;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly ILogger<BranchesController> _logger;
        private readonly BeyadAmi.Server.Application.Validators.CreateBranchValidator _validator;

        public BranchesController(IBranchService branchService, ILogger<BranchesController> logger, BeyadAmi.Server.Application.Validators.CreateBranchValidator validator)
        {
            _branchService = branchService;
            _logger = logger;
            _validator = validator;
        }

        /// <summary>
        /// Get all branches
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BranchDto>), 200)]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var all = await _branchService.GetAllAsync(cancellationToken);
            return Ok(all);
        }

        /// <summary>
        /// Get branch by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BranchDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BranchDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _branchService.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        /// <summary>
        /// Create a new branch
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateBranchDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var errors = _validator.Validate(dto);
            if (System.Linq.Enumerable.Any(errors))
                return BadRequest(new { Errors = errors });

            var newId = await _branchService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Update a branch
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateBranchDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            // simple validation
            if (string.IsNullOrWhiteSpace(dto.BranchName))
                return BadRequest(new { Errors = new[] { "BranchName is required." } });

            await _branchService.UpdateAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete a branch
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _branchService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
