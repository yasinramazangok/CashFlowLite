using CashFlowLite.Application.DTOs;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Services
{
    public interface ITransactionService
    {
        Task<TransactionDto> GetByIdAsync(int id);

        Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIdAsync(int accountId);

        Task<Transaction> LogTransactionAsync(int accountId, decimal amount, TransactionType type);
    }
}
