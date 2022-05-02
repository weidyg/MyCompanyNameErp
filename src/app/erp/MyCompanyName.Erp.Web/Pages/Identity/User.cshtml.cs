using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Identity.Permissions;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Erp.Web.Pages.Identity
{
    [Authorize(IdentityPermissions.Users.Default)]
    public class UsersModel : BasePageModel
    {
        public void OnGet()
        {
        }
    }
}
