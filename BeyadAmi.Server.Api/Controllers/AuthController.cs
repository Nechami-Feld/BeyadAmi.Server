using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeyadAmi.Server.Application.DTOs.Authentication;
using BeyadAmi.Server.Application.Interfaces.Services;

namespace BeyadAmi.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login with username and password.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) return BadRequest("Request body is required.");

            var result = await _authService.LoginAsync(dto, cancellationToken);
            return Ok(result);
        }
    }
}
