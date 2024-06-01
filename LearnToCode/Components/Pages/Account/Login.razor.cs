using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Login
    {
        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        [SupplyParameterFromForm] public LoginViewModel Model { get; set; } = new();

        private string? errorMessage;

        [IgnoreAntiforgeryToken]
        private async Task SubmitFormAsync()
        {
            var user = await appDbContext.User.FirstOrDefaultAsync(user =>
                user.Email == Model.Email && user.Password == AccountHelpers.HashPassword(Model.Password));

            if (user == null)
            {
                errorMessage = "Invalid Email or Password";
                return;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.PermissionLevel),
                new Claim(ClaimTypes.Name, user.Name ?? user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            navigationManager.NavigateTo("/");
        }

        public class LoginViewModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an email")]
            public string? Email { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a password")]
            public string? Password { get; set; }
        }
    }
}
