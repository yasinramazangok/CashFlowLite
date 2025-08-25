using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> LogTransactionAsync(int accountId, decimal amount, TransactionType type)
        {
            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                Type = type,
                CreatedAt = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
            return transaction;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIdAsync(int accountId)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);

            return transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Type = t.Type,
                CreatedAt = t.CreatedAt
            });
        }
    }
}
