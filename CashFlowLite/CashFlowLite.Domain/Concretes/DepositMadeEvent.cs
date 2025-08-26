using CashFlowLite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Domain.Concretes
{
    public class DepositMadeEvent : IDomainEvent
    {
        public int AccountId { get; }
        public decimal Amount { get; }

        public DepositMadeEvent(int accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
