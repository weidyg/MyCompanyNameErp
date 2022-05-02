using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Identity.Permissions;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Erp.Web.Pages.Identity
{
    [Authorize(IdentityPermissions.Roles.Default)]
    public class RolesModel : BasePageModel
    {
        public void OnGet()
        {
        }
    }
}
