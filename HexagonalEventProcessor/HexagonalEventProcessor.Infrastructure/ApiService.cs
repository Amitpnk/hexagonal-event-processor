using HexagonalEventProcessor.Domain.Entities;
using HexagonalEventProcessor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalEventProcessor.Infrastructure
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ProcessedMessage> ProcessMessageAsync(Message message)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://internal-service/api/process", message);

                if (response.IsSuccessStatusCode)
                {
                    var processedMessage = await response.Content.ReadFromJsonAsync<ProcessedMessage>();
                    return processedMessage ?? throw new Exception("Null response received");
                }

                _logger.LogError($"API call failed with status {response.StatusCode}");
                throw new HttpRequestException($"API call failed: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling REST API");
                throw;
            }
        }
    }

}
