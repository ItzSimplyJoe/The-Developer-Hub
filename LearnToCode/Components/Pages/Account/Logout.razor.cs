using Microsoft.AspNetCore.Mvc;

namespace DeveloperHub.Components.Pages.Account
{
    public partial class Logout
    {
        [IgnoreAntiforgeryToken]
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await AuthStateProvider.SignOutAsync();
        }
    
    }
}
