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

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            bool result = await _authService.RegisterAsync(dto);
            if (!result) return BadRequest("Bu email zaten kayıtlı!");
            return Ok("Kullanıcı kaydı başarılı!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResult = await _authService.LoginAsync(dto);
            if (authResult == null) return Unauthorized("Kimlik doğrulama hatası!");
            return Ok(authResult);
        }
    }
}
