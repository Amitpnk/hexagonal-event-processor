using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexagonalEventProcessor.Application;
using HexagonalEventProcessor.Domain.Interfaces;

namespace HexagonalEventProcessor.Api
{
    internal class Program
    {
        services.AddHttpClient<IApiService, ApiService>();  // Register HttpClient for API calls
        services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
        services.AddSingleton<IKafkaProducer, KafkaProducer>();
        services.AddSingleton<MessageProcessorService>();
        services.AddHostedService<ServiceBusWorker>();  // Background worker for consuming messages

    }
}
