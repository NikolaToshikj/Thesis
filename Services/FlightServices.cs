using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

public class FlightService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FlightService> _logger;
    private readonly string _apiKey;

    public FlightService(HttpClient httpClient, ILogger<FlightService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiKey = configuration["FlightApi:ApiKey"];
    }

    public async Task<string> GetOneWayTripAsync(string from, string to, string date, int adults, int children, int infants, string cabinClass, string currency)
    {
        var url = $"https://api.flightapi.io/onewaytrip/{_apiKey}/{from}/{to}/{date}/{adults}/{children}/{infants}/{cabinClass}/{currency}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                _logger.LogError($"Failed to fetch data from FlightAPI: {response.ReasonPhrase}");
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occurred while calling FlightAPI: {ex.Message}");
            return null;
        }
    }
}
