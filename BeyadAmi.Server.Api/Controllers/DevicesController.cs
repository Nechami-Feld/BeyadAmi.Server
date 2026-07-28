using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.Device;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DevicesController(IDeviceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all devices
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), 200)]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get device by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DeviceDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DeviceDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all devices for a specific branch
        /// </summary>
        [HttpGet("branch/{branchId:int}")]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), 200)]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetByBranch(int branchId, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByBranchAsync(branchId, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get available devices for loan in a specific branch
        /// </summary>
        [HttpGet("available/{branchId:int}")]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), 200)]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAvailable(int branchId, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAvailableAsync(branchId, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create a new device
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateDeviceDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var newId = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Update a device
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDeviceDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            await _service.UpdateAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete a device
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
