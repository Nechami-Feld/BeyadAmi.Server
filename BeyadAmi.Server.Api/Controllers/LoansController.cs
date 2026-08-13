using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.Loans;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _service;

        public LoansController(ILoanService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all loans
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), 200)]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get loan by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<LoanDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all active loans
        /// </summary>
        [HttpGet("active")]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), 200)]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetActive(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetActiveAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get loan history for a specific device
        /// </summary>
        [HttpGet("device/{deviceId:int}")]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), 200)]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetByDevice(int deviceId, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByDeviceAsync(deviceId, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a new loan
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateLoanDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var newId = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Return a loaned device
        /// </summary>
        [HttpPut("{id:int}/return")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Return(int id, [FromBody] ReturnLoanDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            await _service.ReturnAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Update a loan
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateLoanDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            // basic validation
            if (string.IsNullOrWhiteSpace(dto.BorrowerLastName))
                return BadRequest(new { Errors = new[] { "BorrowerLastName is required." } });

            if (string.IsNullOrWhiteSpace(dto.Phone))
                return BadRequest(new { Errors = new[] { "Phone is required." } });

            if (dto.DepositTypeId <= 0)
                return BadRequest(new { Errors = new[] { "DepositTypeId is required." } });

            await _service.UpdateAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete a loan
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
