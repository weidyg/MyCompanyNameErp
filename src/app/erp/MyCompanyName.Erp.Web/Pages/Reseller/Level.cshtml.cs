using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Erp.Web.Pages.Reseller
{
    [Authorize(ResellerPermissions.Level.Default)]
    public class LevelModel : BasePageModel
    {
        public void OnGet()
        {
        }
    }
}
