using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Authentication;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken cancellationToken = default);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto, CancellationToken cancellationToken = default);
    }
}
