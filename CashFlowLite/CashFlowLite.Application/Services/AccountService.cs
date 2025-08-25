using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto> GetAccountAsync(int userId)
        {
            var accounts = await _accountRepository.GetAllAsync();
            var account = accounts.FirstOrDefault(a => a.UserId == userId);
            return new AccountDto
            {
                Id = account!.Id,
                Balance = account.Balance
            };
        }

        public async Task<bool> DepositAsync(int accountId, decimal amount)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null) return false;
            account.Balance += amount;
            await _accountRepository.UpdateAsync(account);
            return true;
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null || account.Balance < amount) return false;
            account.Balance -= amount;
            await _accountRepository.UpdateAsync(account);
            return true;
        }
    }
}
