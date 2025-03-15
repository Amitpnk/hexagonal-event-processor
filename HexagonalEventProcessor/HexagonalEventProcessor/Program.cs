using HexagonalEventProcessor;
using HexagonalEventProcessor.Application;
using HexagonalEventProcessor.Domain.Interfaces;
using HexagonalEventProcessor.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ServiceBusWorker>();

builder.Services.AddTransient<IApiService, ApiService>();  // Register HttpClient for API calls
builder.Services.AddTransient<IServiceBusConsumer, ServiceBusConsumer>();
builder.Services.AddTransient<IKafkaProducer, KafkaProducer>();
builder.Services.AddTransient<MessageProcessorService>();
builder.Services.AddHostedService<ServiceBusWorker>();  // Background worker for consuming messages

var host = builder.Build();
host.Run();
