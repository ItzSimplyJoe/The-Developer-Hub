using DeveloperHub.Components.Pages.Account.AccountEnums;
using TimeZone = DeveloperHub.Components.Pages.Account.AccountEnums.TimeZone;

namespace DeveloperHub.ViewModels
{
    public class DateTimeViewModel
    {
        public Language Language { get; set; }

        public TimeZone TimeZone { get; set; }
    }
}
