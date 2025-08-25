using CashFlowLite.Application.DTOs;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CashFlowLite.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Domain.Entities.Transaction> LogTransactionAsync(int accountId, decimal amount, TransactionType type)
        {
            var transaction = new Domain.Entities.Transaction
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

        public async Task<TransactionDto> GetByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null) return null;

            return new TransactionDto
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                Type = transaction.Type,
                CreatedAt = transaction.CreatedAt
            };
        }
    }
}
