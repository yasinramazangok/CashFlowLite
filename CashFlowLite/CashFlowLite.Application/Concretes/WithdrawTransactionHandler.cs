using CashFlowLite.Application.Interfaces;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Concretes;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Concretes
{
    public class WithdrawTransactionHandler : IEventHandler<WithdrawMadeEvent>
    {
        private readonly ITransactionRepository _transactionRepository;

        public WithdrawTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(WithdrawMadeEvent domainEvent)
        {
            var transaction = new Transaction
            {
                AccountId = domainEvent.AccountId,
                Amount = domainEvent.Amount,
                Type = TransactionType.Withdraw,
                CreatedAt = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
        }
    }
}
