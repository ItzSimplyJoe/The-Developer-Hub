using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Login
    {
        private async Task<bool> ConfirmLogin()
        {
            try
            {
                var user = await AppDbContext.User.FirstOrDefaultAsync(u =>
                    LoginModel.Password != null && u.Email == LoginModel.Email && u.Password == AccountHelpers.HashPassword(LoginModel.Password));

                switch (user)
                {
                    case null:
                        _errorMessage = "Invalid Email or Password";
                        return false;
                    case { Name: not null, Role: not null }:
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
                        break;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = "An error occurred during login. Please try again.";
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
