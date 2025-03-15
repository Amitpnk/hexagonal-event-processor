using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexagonalEventProcessor.Domain.Interfaces;

namespace HexagonalEventProcessor.Api
{
    public class ServiceBusWorker : BackgroundService
    {
        private readonly IServiceBusConsumer _serviceBusConsumer;

        public ServiceBusWorker(IServiceBusConsumer serviceBusConsumer)
        {
            _serviceBusConsumer = serviceBusConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _serviceBusConsumer.ConsumeMessageAsync();
        }
    }
}