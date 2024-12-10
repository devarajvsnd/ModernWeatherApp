using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Server.Models; // Import the OpenWeatherMapSettings class
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherController(IOptions<OpenWeatherMapSettings> settings)
        {
            _httpClient = new HttpClient();
            _apiKey = settings.Value.ApiKey;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<object>(content); // Replace `object` with a custom model if needed
                return Ok(weatherData);
            }

            return BadRequest("Unable to fetch weather data.");
        }
    }
}
