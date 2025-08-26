using CashFlowLite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Interfaces
{
    public interface IEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}
