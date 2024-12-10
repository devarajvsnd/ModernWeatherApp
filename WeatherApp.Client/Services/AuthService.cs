using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Login method that calls the backend API and retrieves the JWT token
        public async Task<string> LoginAsync(string email, string password)
        {
            var loginRequest = new
            {
                email = email,
                password = password
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            // Call the backend API to authenticate the user
            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<LoginResponse>(responseContent)?.Token;

                return token;
            }

            return null;  // If login fails, return null
        }

        // Register method
        public async Task RegisterAsync(string email, string password)
        {
            var registerRequest = new
            {
                email = email,
                password = password
            };

            var content = new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json");

            // Call the backend API to register the user
            var response = await _httpClient.PostAsync("api/auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                // Handle registration failure
                throw new Exception("Registration failed");
            }
        }

        // Logout method (clear token)
        public async Task LogoutAsync()
        {
            // Here you can remove the token from local storage or session storage
            await Task.CompletedTask;
        }

        // Helper class to parse login response
        public class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}
