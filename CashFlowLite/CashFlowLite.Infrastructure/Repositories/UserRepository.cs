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
    public class UserRepository : IUserRepository
    {
        private readonly CashFlowLiteDbContext _context;

        public UserRepository(CashFlowLiteDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id) =>
            await _context.Users.Include(u => u.Accounts).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
