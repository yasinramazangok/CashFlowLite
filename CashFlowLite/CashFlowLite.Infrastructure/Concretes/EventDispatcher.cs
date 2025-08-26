using CashFlowLite.Application.Interfaces;
using CashFlowLite.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Infrastructure.Concretes
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch(IDomainEvent domainEvent)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = (IEnumerable<dynamic>)_serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await handler.Handle((dynamic)domainEvent);
            }
        }
    }
}
