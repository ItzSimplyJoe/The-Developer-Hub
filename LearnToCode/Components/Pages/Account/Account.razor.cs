using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DeveloperHub.ViewModels;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Account
    {
        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        public User? User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (HttpContext != null)
            {
                var input = HttpContext.User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.NameIdentifier)?.Value;

                if (input == null)
                {
                    navigationManager.NavigateTo("/Not-Found");
                    return;
                }

                var id = Guid.Parse(input);
                var user = await AppDbContext.User.FirstOrDefaultAsync(user => user.Id == id).ConfigureAwait(false);
                if (user == null)
                {
                    navigationManager.NavigateTo("/Not-Found");
                    return;
                }

                User = user;
            }

            UserSettingsModel.Email = User?.Email;
            UserSettingsModel.FirstName = User?.FirstName;
            UserSettingsModel.LastName = User?.LastName;
            UserSettingsModel.PhoneNumber = User?.PhoneNumber;
            UserSettingsModel.Address = User?.Address;
            UserSettingsModel.City = User?.City;
            UserSettingsModel.Country = User?.Country;
            UserSettingsModel.PostalCode = User?.PostalCode;
            UserSettingsModel.Organisation = User?.Organisation;
            UserSettingsModel.Role = User?.Role;
            UserSettingsModel.Department = User?.Department;
            UserSettingsModel.Birthday = User?.Birthday;

            await InvokeAsync(StateHasChanged);
        }

        public async Task UpdateUserDetails()
        {
            if (User != null)
            {
                if (UserSettingsModel.Email != null) 
                    User.Email = UserSettingsModel.Email;
                User.FirstName = UserSettingsModel.FirstName;
                User.LastName = UserSettingsModel.LastName;
                User.PhoneNumber = UserSettingsModel.PhoneNumber;
                User.Address = UserSettingsModel.Address;
                User.City = UserSettingsModel.City;
                User.Country = UserSettingsModel.Country;
                User.PostalCode = UserSettingsModel.PostalCode;
                User.Organisation = UserSettingsModel.Organisation;
                User.Role = UserSettingsModel.Role;
                User.Department = UserSettingsModel.Department;
                User.Birthday = UserSettingsModel.Birthday;

                AppDbContext.User.Update(User);
                await AppDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateProfilePictureAsync()
        {
            if (_photoURL == null)
            {
                _errorMessagePFP = "You need to upload a profile picture.";
                return;
            }
            if (User != null)
            {
                User.ImageBytes = _photoURL;
                AppDbContext.User.Update(User);
            }

            await AppDbContext.SaveChangesAsync().ConfigureAwait(false);

        }

        public async Task UpdatePassword()
        {
            if (string.IsNullOrWhiteSpace(PasswordUpdateModel.OldPassword))
            {
                _errorMessagePassword = "Old password is required";
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordUpdateModel.NewPassword))
            {
                _errorMessagePassword = "New password is required";
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordUpdateModel.ConfirmPassword))
            {
                _errorMessagePassword = "Confirm Password is required";
                return;
            }

            if (PasswordUpdateModel.NewPassword != PasswordUpdateModel.ConfirmPassword)
            {
                _errorMessagePassword = "Passwords do not match";
                return;
            }

            if (!PasswordValidator.ValidatePassword(PasswordUpdateModel.NewPassword))
            {
                _errorMessagePassword = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character";
                return;
            }

            if (User != null)
            {
                User.Password = PasswordUpdateModel.NewPassword;
                AppDbContext.User.Update(User);
            }

            await AppDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateTimeAndLanguage()
        {
            if (User != null)
            {
                User.Language = DateTimeViewModel.Language;
                User.TimeZone = DateTimeViewModel.TimeZone;
                AppDbContext.User.Update(User);
            }

            await AppDbContext.SaveChangesAsync().ConfigureAwait(false);
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
}
