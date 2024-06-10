using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace DeveloperHub
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NavigationManager _navigationManager;

        public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor, NavigationManager navigationManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task SignInAsync(string email, string role, string name, string userId)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role),
                new(ClaimTypes.Name, name),
                new(ClaimTypes.NameIdentifier, userId)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            if (!_httpContextAccessor.HttpContext.Response.HasStarted)
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));

                _navigationManager.NavigateTo("/");
            }
        }

        public async Task SignOutAsync()
        {
            if (!_httpContextAccessor.HttpContext.Response.HasStarted)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var identity = new ClaimsIdentity();
                var user = new ClaimsPrincipal(identity);

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

                _navigationManager.NavigateTo("/login");
            }
        }

        public async Task<Guid?> GetUserIdAsync()
        {
            var user = await GetAuthenticationStateAsync();
            var userIdClaim = user.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }
    }
}
