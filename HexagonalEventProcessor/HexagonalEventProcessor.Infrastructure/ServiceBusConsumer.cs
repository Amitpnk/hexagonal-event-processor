using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexagonalEventProcessor.Application;
using HexagonalEventProcessor.Domain.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Message = HexagonalEventProcessor.Domain.Entities.Message;

namespace HexagonalEventProcessor.Infrastructure
{
    public class ServiceBusConsumer : IServiceBusConsumer
    {
        private readonly IQueueClient _queueClient;
        private readonly MessageProcessorService _processorService;

        public ServiceBusConsumer(IConfiguration config, MessageProcessorService processorService)
        {
            var connectionString = config["AzureServiceBus:ConnectionString"];
            var queueName = config["AzureServiceBus:QueueName"];
            _queueClient = new QueueClient(connectionString, queueName);
            _processorService = processorService;
        }

        public async Task ConsumeMessageAsync()
        {
            _queueClient.RegisterMessageHandler(async (message, token) =>
                {
                    var payload = Encoding.UTF8.GetString(message.Body);
                    var receivedMessage = JsonConvert.DeserializeObject<Message>(payload);
                    await _processorService.HandleMessageAsync(receivedMessage);
                    await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
                },
                new MessageHandlerOptions(args => Task.CompletedTask) { AutoComplete = false });
        }
    }
}