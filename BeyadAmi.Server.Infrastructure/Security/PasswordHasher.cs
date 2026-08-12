using System;
using System.Security.Cryptography;
using BeyadAmi.Server.Application.Interfaces;

namespace BeyadAmi.Server.Infrastructure.Security
{
    // Simple PBKDF2 wrapper using Rfc2898DeriveBytes. Stores: iterations.salt.hash (Base64)
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            using var deriveBytes = new Rfc2898DeriveBytes(password ?? string.Empty, salt, Iterations, HashAlgorithmName.SHA256);
            var key = deriveBytes.GetBytes(KeySize);
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public bool Verify(string hash, string password)
        {
            if (string.IsNullOrEmpty(hash)) return false;
            var parts = hash.Split('.');
            if (parts.Length != 3) return false;
            if (!int.TryParse(parts[0], out var iterations)) return false;
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var deriveBytes = new Rfc2898DeriveBytes(password ?? string.Empty, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = deriveBytes.GetBytes(KeySize);
            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }
    }
}

