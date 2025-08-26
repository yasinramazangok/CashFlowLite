using CashFlowLite.Application.DTOs;

namespace CashFlowLite.Application.Services
{
    public interface IAuthService
    {
        Task<int> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
