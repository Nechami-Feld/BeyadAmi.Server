using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.DTOs.Device;
using BeyadAmi.Server.Application.Validators;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly ILogger<DevicesController> _logger;
        private readonly CreateDeviceValidator _validator;

        public DevicesController(IDeviceService deviceService, ILogger<DevicesController> logger, CreateDeviceValidator validator)
        {
            _deviceService = deviceService;
            _logger = logger;
            _validator = validator;
        }

        /// <summary>
        /// Get all devices
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), 200)]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var devices = await _deviceService.GetAllAsync(cancellationToken);
            return Ok(devices);
        }

        /// <summary>
        /// Get device by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DeviceDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DeviceDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var device = await _deviceService.GetByIdAsync(id, cancellationToken);
            if (device == null)
                return NotFound();
            return Ok(device);
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

            var errors = _validator.Validate(dto).ToArray();
            if (errors.Any())
                return BadRequest(new { Errors = errors });

            var newId = await _deviceService.CreateAsync(dto, cancellationToken);
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

            if (dto.DeviceId != id)
                return BadRequest("Id mismatch.");

            // reuse create validator for basic validation of fields
            var errors = _validator.Validate(new CreateDeviceDto
            {
                DeviceTypeId = dto.DeviceTypeId,
                BranchId = dto.BranchId,
                DeviceNumber = dto.DeviceNumber,
                Company = dto.Company,
                Notes = dto.Notes
            }).ToArray();

            if (errors.Any())
                return BadRequest(new { Errors = errors });

            await _deviceService.UpdateAsync(dto, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Delete a device
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _deviceService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
