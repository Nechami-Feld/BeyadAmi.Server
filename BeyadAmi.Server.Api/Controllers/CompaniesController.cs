using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.DTOs.Companies;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompaniesController> _logger;
        private readonly BeyadAmi.Server.Application.Validators.CreateCompanyValidator _validator;

        public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger, BeyadAmi.Server.Application.Validators.CreateCompanyValidator validator)
        {
            _companyService = companyService;
            _logger = logger;
            _validator = validator;
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDto>), 200)]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var all = await _companyService.GetAllAsync(cancellationToken);
            return Ok(all);
        }

        /// <summary>
        /// Get company by id
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CompanyDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _companyService.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CreateCompanyDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            var errors = _validator.Validate(dto);
            if (System.Linq.Enumerable.Any(errors))
                return BadRequest(new { Errors = errors });

            var newId = await _companyService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        /// <summary>
        /// Update a company
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCompanyDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(dto.CompanyName))
                return BadRequest(new { Errors = new[] { "CompanyName is required." } });

            await _companyService.UpdateAsync(id, dto, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete a company
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _companyService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
