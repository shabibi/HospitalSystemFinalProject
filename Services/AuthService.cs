using HospitalSystemTeamTask.Helper;
using HospitalSystemTeamTask.Models;
using JWTAuthentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalSystemTeamTask.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthService(JwtSettings jwtSettings, IJSRuntime jsRuntime, AuthenticationStateProvider authenticationStateProvider)
        {
            _jwtSettings = jwtSettings;
            _jsRuntime = jsRuntime;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public JwtTokenResponse GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.UID.ToString()),
            new Claim(ClaimTypes.Role, user.Role) // Example role
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            SaveTokenToCookie(tokenString);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(tokenString);

            JwtTokenResponse response;
            response = new JwtTokenResponse { Token = tokenString, Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes) };
            return response;
        }

        public async Task SaveTokenToCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    // Prevents client-side access to the cookie (reduces XSS risk)
                Secure = true,      // Set to true in production (to ensure the cookie is only sent over HTTPS)
                Expires = DateTime.UtcNow.AddHours(1),  // Adjust expiry based on your needs
                SameSite = SameSiteMode.Strict // Protect against CSRF attacks
            };

            // Store the token in a cookie
            await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = 'authToken={token}; {cookieOptions.ToString()}';");
        }
    }
}
