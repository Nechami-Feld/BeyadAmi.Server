using System;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<(string token, DateTime expiresAt)> CreateTokenAsync(User user);
    }
}
