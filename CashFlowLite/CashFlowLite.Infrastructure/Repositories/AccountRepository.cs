using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CashFlowLite.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CashFlowLiteDbContext _context;

        public AccountRepository(CashFlowLiteDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(int accountId) =>
            await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.Id == accountId);

        public async Task<IEnumerable<Account>> GetAllAsync() =>
            await _context.Accounts.ToListAsync();

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Account> GetAccountByUserIdAsync(int userId) =>
            await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.UserId == userId);
    }
}
