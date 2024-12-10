//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using WeatherApp.Server.Models; // Your user/auth models


namespace WeatherApp.Server.Controllers
{
    

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly Client _supabaseClient;

        public AuthController(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _supabaseClient.Auth.SignUp(request.Email, request.Password);
            if (result.User == null)
            {
                return BadRequest(new { message =  "Registration failed." });
                
            }
            return Ok(new { message = "Registration successful", user = result.User });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _supabaseClient.Auth.SignInWithPassword(request.Email, request.Password);
            if (result.User == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            return Ok(new { message = "Login successful", token = result.AccessToken });
        }
    }


}
