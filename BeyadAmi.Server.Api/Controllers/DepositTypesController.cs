using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.DTOs.DepositType;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositTypesController : ControllerBase
    {
        private readonly IDepositTypeService _service;

        public DepositTypesController(IDepositTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepositTypeDto>), 200)]
        public async Task<ActionResult<IEnumerable<DepositTypeDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }
       
    }
}
