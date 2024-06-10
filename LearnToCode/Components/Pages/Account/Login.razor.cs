using System;
using System.Security.Claims;
using DeveloperHub.Data;
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
        private readonly CreateValidator _validator = new();
        private readonly LoginViewModel _model = new();
        private string? _errorMessage;

        [IgnoreAntiforgeryToken]
        private async Task SubmitFormAsync()
        {
            try
            {
                var user = await AppDbContext.User.FirstOrDefaultAsync(u =>
                    u.Email == _model.Email && u.Password == AccountHelpers.HashPassword(_model.Password));

                if (user == null)
                {
                    _errorMessage = "Invalid Email or Password";
                    return;
                }

                await AuthStateProvider.SignInAsync(user.Email, user.PermissionLevel, user.Name, user.Id.ToString());
            }
            catch (Exception ex)
            {
                _errorMessage = "An error occurred during login. Please try again.";
                Console.WriteLine(ex);
            }
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
