using BlazorInputFile;
using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        public async Task SubmitUserDetails()
        {
            Console.Write("HLeoo");
        }
    }

    public class UserSettingsModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an email")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter your first name")]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a last name")]
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
}
