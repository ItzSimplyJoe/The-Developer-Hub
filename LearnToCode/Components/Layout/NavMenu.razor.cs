using Microsoft.AspNetCore.Components;

namespace DeveloperHub.Components.Layout
{
    public partial class NavMenu
    {
        public Guid? UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();
            var userId = await AuthStateProvider.GetUserIdAsync();
            if (userId != Guid.Empty)
                UserId = userId;
        }
    }
}
