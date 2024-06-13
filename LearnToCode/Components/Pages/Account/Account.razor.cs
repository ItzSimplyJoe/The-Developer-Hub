using BlazorInputFile;
using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DeveloperHub.ViewModels;

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

            var input = HttpContext.User.Claims.FirstOrDefault(item => item.Type == ClaimTypes.NameIdentifier).Value;

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
}
