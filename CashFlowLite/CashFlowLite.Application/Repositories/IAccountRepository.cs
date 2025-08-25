using CashFlowLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(int id);
        Task<IEnumerable<Account>> GetAllAsync();
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(int id);
    }
}
