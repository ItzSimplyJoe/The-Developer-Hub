using System.Security.Claims;
using FluentValidation;
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

        private readonly CreateValidator _validator = new();

        private readonly LoginViewModel _model = new();

        private string? _errorMessage;

        [IgnoreAntiforgeryToken]
        private async Task SubmitFormAsync()
        {
            var user = await appDbContext.User.FirstOrDefaultAsync(user =>
                user.Email == _model.Email && user.Password == AccountHelpers.HashPassword(_model.Password));

            if (user == null)
            {
                _errorMessage = "Invalid Email or Password";
                return;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.PermissionLevel),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            navigationManager.NavigateTo("/");
        }

        public class LoginViewModel
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        private class CreateValidator : AbstractValidator<LoginViewModel>
        {
            public CreateValidator()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            }
        }
    }
}
