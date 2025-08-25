using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowLite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOnboardingService _onboardingService;

        public AuthsController(IAuthService authService, IOnboardingService onboardingService)
        {
            _authService = authService;
            _onboardingService = onboardingService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var userId = await _onboardingService.RegisterAndCreateAccountAsync(dto);
            return Ok($"Kullanıcı {userId} numaralı ID ile oluşturuldu!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }
    }
}
