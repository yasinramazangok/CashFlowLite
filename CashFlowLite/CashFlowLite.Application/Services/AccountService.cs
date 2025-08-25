using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Domain.Enums;
using System.Security.Principal;

namespace CashFlowLite.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;

        public AccountService(IAccountRepository accountRepository, ITransactionService transactionService)
        {
            _accountRepository = accountRepository;
            _transactionService = transactionService;
        }

        public async Task<AccountDto> GetAccountByUserIdAsync(int userId)
        {
            var account = await _accountRepository.GetAccountByUserIdAsync(userId);
            if (account == null)
                return null;

            return new AccountDto
            {
                Id = account.Id,
                UserId = account.UserId,
                Balance = account.Balance
            };
        }

        public async Task<bool> DepositAsync(int accountId, decimal amount)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null) return false;
            account.Balance += amount;
            await _accountRepository.UpdateAsync(account);

            // Service-to-Service call → Transaction log
            await _transactionService.LogTransactionAsync(accountId, amount, TransactionType.Deposit);

            return true;
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null || account.Balance < amount || account.Balance < 0) return false;
            account.Balance -= amount;
            await _accountRepository.UpdateAsync(account);

            // Service-to-Service call → Transaction log
            await _transactionService.LogTransactionAsync(accountId, amount, TransactionType.Withdraw);

            return true;
        }

        public async Task<int> CreateAccountAsync(int userId)
        {
            var account = new Account 
            { 
                UserId = userId, 
                Balance = 0, 
                CreatedAt = DateTime.UtcNow 
            };
            
            await _accountRepository.AddAsync(account);
            return account.Id;
        }
    }
}
