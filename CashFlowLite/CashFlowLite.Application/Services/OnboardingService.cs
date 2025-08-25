using CashFlowLite.Application.DTOs;

namespace CashFlowLite.Application.Services
{
    public class OnboardingService : IOnboardingService
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public OnboardingService(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        public async Task<int> RegisterAndCreateAccountAsync(RegisterDto dto)
        {
            var userId = await _authService.RegisterAsync(dto);

            await _accountService.CreateAccountAsync(userId);

            return userId;
        }
    }
}
