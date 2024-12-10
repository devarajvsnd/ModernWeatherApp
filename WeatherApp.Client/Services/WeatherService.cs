using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WeatherApp.Client.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> GetCurrentWeather(string city)
        {
            return await _httpClient.GetFromJsonAsync<object>($"api/weather/current?city={city}");
        }
    }
}
