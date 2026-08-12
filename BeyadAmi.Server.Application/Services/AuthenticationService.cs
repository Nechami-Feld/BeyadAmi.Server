using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Authentication;
using BeyadAmi.Server.Application.Interfaces;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Exceptions;

namespace BeyadAmi.Server.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new System.ArgumentNullException(nameof(dto));

            var user = await _userRepository.GetByUserNameAsync(dto.UserName ?? string.Empty, cancellationToken);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                throw new InvalidCredentialsException();

            if (!user.IsActive)
                throw new InvalidCredentialsException();

            var verified = _passwordHasher.Verify(user.PasswordHash!, dto.Password ?? string.Empty);
            if (!verified)
                throw new InvalidCredentialsException();

            var (token, expiresAt) = await _jwtTokenService.CreateTokenAsync(user);

            return new LoginResponseDto
            {
                AccessToken = token,
                ExpiresAt = expiresAt,
                UserId = user.UserId,
                UserName = user.UserName
            };
        }
    }
}
