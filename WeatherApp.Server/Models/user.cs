namespace WeatherApp.Server.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class OpenWeatherMapSettings
    {
        public string ApiKey { get; set; } // OpenWeatherMap API key
    }
}
