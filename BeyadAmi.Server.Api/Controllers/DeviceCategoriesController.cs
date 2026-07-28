using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceCategoriesController : ControllerBase
    {
        private readonly IDeviceCategoryService _service;

        public DeviceCategoriesController(IDeviceCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeviceCategoryDto>), 200)]
        public async Task<ActionResult<IEnumerable<DeviceCategoryDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DeviceCategoryDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DeviceCategoryDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateDeviceCategoryDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var newId = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDeviceCategoryDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            await _service.UpdateAsync(id, dto, cancellationToken);
            return Ok();
        }

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
