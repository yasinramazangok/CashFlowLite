using CashFlowLite.Application.Interfaces;
using CashFlowLite.Application.Repositories;
using CashFlowLite.Domain.Concretes;
using CashFlowLite.Domain.Entities;
using CashFlowLite.Domain.Enums;

namespace CashFlowLite.Application.Concretes
{
    public class DepositTransactionHandler : IEventHandler<DepositMadeEvent>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DepositTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(DepositMadeEvent domainEvent)
        {
            var transaction = new Transaction
            {
                AccountId = domainEvent.AccountId,
                Amount = domainEvent.Amount,
                Type = TransactionType.Deposit,
                CreatedAt = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
        }
    }
}
