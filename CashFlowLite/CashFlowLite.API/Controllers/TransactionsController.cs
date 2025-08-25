using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowLite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            return transaction == null
                ? NotFound($"'{id}' numaralı ID'ye sahip işlem bulunamadı!")
                : Ok(transaction);
        }

        // GET api/transactions/{id}
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactionsByAccountIdAsync(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
            return transactions == null
                ? NotFound($"'{accountId}' numaralı hesaba ait işlem bulunamadı!")
                : Ok(transactions);
        }
    }
}
