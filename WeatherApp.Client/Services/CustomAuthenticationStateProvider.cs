using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;


namespace WeatherApp.Client.Services
{
    public class CustomAuthenticationStateProvider: AuthenticationStateProvider
    {
        private readonly IAuthService _authService; // Service to handle API calls for login/register
        private readonly ILocalStorageService _localStorageService; // For storing authentication tokens

        public CustomAuthenticationStateProvider(IAuthService authService, ILocalStorageService localStorageService)
        {
            _authService = authService;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Try to get the token from local storage
            var token = await _localStorageService.GetItemAsync<string>("auth_token");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // No user logged in
            }

            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "User") }, "Bearer");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user); // User logged in with valid token
        }

        // Call this method to notify other components about authentication state changes
        public void MarkUserAsAuthenticated(string token)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "User") }, "Bearer");
            var user = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(user);

            // Store the token in local storage
            _localStorageService.SetItemAsync("auth_token", token);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState)); // Notify state change
        }

        // Call this method to log out the user
        public void MarkUserAsLoggedOut()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(user);

            _localStorageService.RemoveItemAsync("auth_token");
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState)); // Notify state change
        }
    }
}