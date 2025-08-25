using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowLite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET api/accounts/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAccountByUserId(int userId)
        {
            var account = await _accountService.GetAccountByUserIdAsync(userId);
            return account == null
                ? NotFound($"'{userId}' numaralı ID'ye sahip kullanıcı ve hesabı bulunamadı!")
                : Ok(account);
        }

        // POST api/accounts/{accountId}/deposit
        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(int accountId, [FromBody] AmountDto dto)
        {
            var success = await _accountService.DepositAsync(accountId, dto.Amount);
            if (!success) return BadRequest("İşlem başarısız!");
            return Ok("Para yatırma işlemi başarılı!");
        }

        // POST api/accounts/{accountId}/withdraw
        [HttpPost("{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, [FromBody] AmountDto dto)
        {
            var success = await _accountService.WithdrawAsync(accountId, dto.Amount);
            if (!success) return BadRequest("İşlem başarısız!");
            return Ok("Para çekme işlemi başarılı!");
        }
    }
}
