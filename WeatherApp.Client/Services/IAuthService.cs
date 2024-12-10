namespace WeatherApp.Client.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task RegisterAsync(string email, string password);
        Task LogoutAsync();
    }
}
