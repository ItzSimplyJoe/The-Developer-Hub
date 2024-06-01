using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Signup
    {
        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        [SupplyParameterFromForm] public SignupViewModel Model { get; set; } = new();

        private string? errorMessage;

        [IgnoreAntiforgeryToken]
        private async Task SubmitFormAsync()
        {
            if (Model.Email == null || Model.Password == null)
            {
                errorMessage = "Ensure all fields are completed";
                return;
            }

            if (await appDbContext.User.Where(user => user.Email == Model.Email).AnyAsync().ConfigureAwait(false))
            {
                errorMessage = "Account with this email already exists";
                return;
            }

            var user = new User()
            {
                Email = Model.Email,
                Password = AccountHelpers.HashPassword(Model.Password),
                PermissionLevel = "User",
            };

            await appDbContext.User.AddAsync(user).ConfigureAwait(false);
            await appDbContext.SaveChangesAsync().ConfigureAwait(false);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.PermissionLevel),
                new Claim(ClaimTypes.Name, user.Name ?? user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            navigationManager.NavigateTo("/");
        }

        public class SignupViewModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an email")]
            public string? Email { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a password")]
            public string? Password { get; set; }
        }
    }
}