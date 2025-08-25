using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CashFlowLite.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null) return -1;

            // Generate salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            // Password hashing
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: dto.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hashed,
                PasswordSalt = salt,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: dto.Password,
                salt: user.PasswordSalt, // demo salt
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (hashed != user.PasswordHash) return null;

            string token = "demo-jwt-token";
            return new AuthResponseDto { Token = token };
        }
    }
}
