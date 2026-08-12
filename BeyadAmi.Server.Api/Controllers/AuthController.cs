using System.Threading;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
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
        /// Returns current authenticated user info.
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public ActionResult<object> Me()
        {
            var userId = User.FindFirst("userId")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst("userName")?.Value ?? User.Identity?.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            return Ok(new { UserId = int.Parse(userId), UserName = userName, Role = role });
        }

        /// <summary>
        /// Admin test endpoint. Intended for testing only.
        /// </summary>
        [HttpGet("admin-test")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public ActionResult AdminTest()
        {
            return Ok();
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

        /// <summary>
        /// Register a new user.
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponseDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) return BadRequest("Request body is required.");

            var result = await _authService.RegisterAsync(dto, cancellationToken);
            return Created(string.Empty, result);
        }
    }
}
