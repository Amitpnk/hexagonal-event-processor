using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexagonalEventProcessor.Domain.Entities;
using HexagonalEventProcessor.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HexagonalEventProcessor.Application
{
    public class MessageProcessorService
    {
        private readonly IServiceBusConsumer _serviceBusConsumer;
        private readonly IApiService _apiService;
        private readonly ILogger<MessageProcessorService> _logger;
        private readonly IKafkaProducer _kafkaProducer;

        public MessageProcessorService(IServiceBusConsumer serviceBusConsumer,
            IApiService apiService, ILogger<MessageProcessorService> logger,
            IKafkaProducer kafkaProducer)
        {
            _serviceBusConsumer = serviceBusConsumer;
            _apiService = apiService;
            _logger = logger;
            _kafkaProducer = kafkaProducer;
        }

        public async Task ProcessMessagesAsync()
        {
            await _serviceBusConsumer.ConsumeMessageAsync();
        }

        public async Task HandleMessageAsync(Message message)
        {
            try
            {
                _logger.LogInformation($"Processing message: {message.Id}");

                var processedMessage = await _apiService.ProcessMessageAsync(message);

                // Publish to Kafka topic based on processing
                await _kafkaProducer.SendMessageAsync("business-topic", processedMessage.Payload);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
            }
        }
    }

}
