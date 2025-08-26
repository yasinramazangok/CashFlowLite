using CashFlowLite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Interfaces
{
    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
