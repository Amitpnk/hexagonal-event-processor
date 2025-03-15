using HexagonalEventProcessor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace HexagonalEventProcessor.Infrastructure
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(IConfiguration config)
        {
            var configDict = new ProducerConfig { BootstrapServers = config["Kafka:Broker"] };
            _producer = new ProducerBuilder<Null, string>(configDict).Build();
        }

        public async Task SendMessageAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }

}
