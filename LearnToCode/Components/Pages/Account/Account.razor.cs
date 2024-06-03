using BlazorInputFile;
using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DeveloperHub.Components.Pages.Account.Signup;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Account
    {
        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        public User? User { get; set; }

        [SupplyParameterFromForm]
        public UserSettingsModel UserSettingsModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var input = HttpContext?.User.Claims.FirstOrDefault(item => item.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (input == null)
            {
                navigationManager.NavigateTo("/Not-Found");
                return;
            }
            var id = Guid.Parse(input);
            if (id == Guid.Empty)
            {
                navigationManager.NavigateTo("/Not-Found");
                return;
            }

            var user = await appDbContext.User.FirstOrDefaultAsync(user => user.Id == id).ConfigureAwait(false);
            if (user == null)
            {
                navigationManager.NavigateTo("/Not-Found");
                return;
            }

            User = user;

            UserSettingsModel.Email = User.Email;
            UserSettingsModel.FirstName = User.FirstName;
            UserSettingsModel.LastName = User.LastName;
            UserSettingsModel.PhoneNumber = User.PhoneNumber;
            UserSettingsModel.Address = User.Address;
            UserSettingsModel.City = User.City;
            UserSettingsModel.Country = User.Country;
            UserSettingsModel.PostalCode = User.PostalCode;
            UserSettingsModel.Organisation = User.Organisation;
            UserSettingsModel.Role = User.Role;
            UserSettingsModel.Department = User.Department;
            UserSettingsModel.Birthday = User.Birthday;

            await InvokeAsync(StateHasChanged);
        }

        [IgnoreAntiforgeryToken]
        public async Task SubmitUserDetailsAsync()
        {
            Console.Write("HLeoo");
        }

        public async Task UpadteProfilePictureAsync()
        {
            Console.Write("Hello world!");
        }

        public async Task UpdatePasswordAsync()
        {
            Console.Write("Hello world!");
        }

        public async Task AddFriendAsync()
        {
            Console.Write("Hello world!");
        }

        public async Task RemoveFriendAsync()
        {
            Console.Write("Hello world!");
        }

        public async Task SearchFriendAsync()
        {
            Console.Write("Hello world!");
        }

        public async Task AddSocialAccountAsync()
        {
            Console.Write("Hello world!");
        }

        public async Task RemoveSocialAccountAsync()
        {
            Console.Write("Hello world!");
        }
    }

    public class UserSettingsModel
    {
        public string? Email { get; set; }
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

    public class PasswordUpdateModel 
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class CreateValidator : AbstractValidator<PasswordUpdateModel>
    {
        public CreateValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old Password is required.")
                .Equal(x => x.Password).WithMessage("Old Password doesn't match.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one symbol");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.NewPassword).WithMessage("Confirm Password must match Password");
        }
    }
}
