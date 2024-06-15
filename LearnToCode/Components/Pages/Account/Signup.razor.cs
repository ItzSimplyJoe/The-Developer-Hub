using System.Security.Claims;
using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Signup
    {
        private async Task ConfirmSignupAsync()
        {
            if (await AppDbContext.User.AnyAsync(user => user.Email == Model.Email).ConfigureAwait(false))
            {
                _errorMessage = "Account with this email already exists";
                return;
            }

            if (Model is { Email: not null, Password: not null })
            {
                var user = new User
                {
                    Email = Model.Email,
                    Password = AccountHelpers.HashPassword(Model.Password),
                    PermissionLevel = "User",
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    PhoneNumber = Model.PhoneNumber,
                    Address = Model.Address,
                    City = Model.City,
                    Country = Model.Country,
                    PostalCode = Model.PostalCode,
                    Organisation = Model.Organisation,
                    Role = Model.Role,
                    Department = Model.Department,
                    Birthday = Model.Birthday,
                    Name = $"{Model.FirstName} {Model.LastName}",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                };

                await AppDbContext.User.AddAsync(user).ConfigureAwait(false);
                await AppDbContext.SaveChangesAsync().ConfigureAwait(false);

                if (user.Role != null)
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, user.Name),
                        new(ClaimTypes.Role, user.Role),
                        new(ClaimTypes.Email, user.Email),
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    if (HttpContext != null) 
                        await HttpContext.SignInAsync(principal);
                }
            }

            navigationManager.NavigateTo("/");
        }
    }
}
