using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CashFlowLiteDbContext _context;

        public TransactionRepository(CashFlowLiteDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetByIdAsync(int id) =>
            await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId) =>
            await _context.Transactions
                          .Where(t => t.AccountId == accountId)
                          .ToListAsync();

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
