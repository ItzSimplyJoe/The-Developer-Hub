using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Logout
    {
        [IgnoreAntiforgeryToken]
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (HttpContextService.HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContextService.HttpContext.SignOutAsync();
                navigationManager.NavigateTo("/logout", true);
            }
        }
    
    }
}
