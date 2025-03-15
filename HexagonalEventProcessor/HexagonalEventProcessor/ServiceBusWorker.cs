using HexagonalEventProcessor.Domain.Interfaces;

namespace HexagonalEventProcessor
{
    public class ServiceBusWorker : BackgroundService
    {
        private readonly ILogger<ServiceBusWorker> _logger;
        private readonly IServiceBusConsumer _serviceBusConsumer;

        public ServiceBusWorker(ILogger<ServiceBusWorker> logger, IServiceBusConsumer serviceBusConsumer)
        {
            _logger = logger;
            _serviceBusConsumer = serviceBusConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _serviceBusConsumer.ConsumeMessageAsync();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
