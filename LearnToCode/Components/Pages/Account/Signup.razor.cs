using System.Security.Claims;
using DeveloperHub.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Signup
    {
        private readonly SignupViewModel _model = new();
        private readonly CreateValidator _validator = new();

        private string? _errorMessage;

        private async Task SubmitFormAsync()
        {
            if (await AppDbContext.User.AnyAsync(user => user.Email == _model.Email).ConfigureAwait(false))
            {
                _errorMessage = "Account with this email already exists";
                return;
            }

            var user = new User
            {
                Email = _model.Email,
                Password = AccountHelpers.HashPassword(_model.Password),
                PermissionLevel = "User",
                FirstName = _model.FirstName,
                LastName = _model.LastName,
                PhoneNumber = _model.PhoneNumber,
                Address = _model.Address,
                City = _model.City,
                Country = _model.Country,
                PostalCode = _model.PostalCode,
                Organisation = _model.Organisation,
                Role = _model.Role,
                Department = _model.Department,
                Birthday = _model.Birthday,
                Name = $"{_model.FirstName} {_model.LastName}",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
            };

            await AppDbContext.User.AddAsync(user).ConfigureAwait(false);
            await AppDbContext.SaveChangesAsync().ConfigureAwait(false);

            if (user.Email != null)
                await AuthStateProvider.SignInAsync(user.Email, user.PermissionLevel, user.Name, user.Id.ToString());
            NavigationManager.NavigateTo("/");
        }

        public class SignupViewModel
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? ConfirmPassword { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? Organisation { get; set; }
            public string? Role { get; set; }
            public string? Department { get; set; }
            public DateTime? Birthday { get; set; }
        }

        private class CreateValidator : AbstractValidator<SignupViewModel>
        {
            public CreateValidator()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Must enter a valid email");
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                    .Matches("[0-9]").WithMessage("Password must contain at least one number")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one symbol");
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required")
                    .Equal(x => x.Password).WithMessage("Confirm Password must match Password");
                RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            }
        }
    }
}
