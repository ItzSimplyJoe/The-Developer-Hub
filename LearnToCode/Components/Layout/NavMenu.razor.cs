using Microsoft.AspNetCore.Components;

namespace DeveloperHub.Components.Layout
{
    public partial class NavMenu
    {
        public string UserId { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitializedAsync();
            var userId = HttpContextService.HttpContext?.User.Claims.FirstOrDefault(user => user.Type == "NameIdentifier")?.Value;
            if (userId != null)
                UserId = userId;
        }
    }
}
