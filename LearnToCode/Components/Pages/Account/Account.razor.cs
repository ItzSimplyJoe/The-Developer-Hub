using BlazorInputFile;
using DeveloperHub.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

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
            await InvokeAsync(StateHasChanged);
        }
    }
}
