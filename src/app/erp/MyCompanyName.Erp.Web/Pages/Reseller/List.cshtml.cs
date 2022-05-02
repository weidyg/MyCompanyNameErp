using Microsoft.AspNetCore.Authorization;
using MyCompanyName.Erp.Permissions;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Erp.Web.Pages.Reseller
{
    [Authorize(ResellerPermissions.Reseller.Default)]
    public class ListModel : BasePageModel
    {
        public void OnGet()
        {
        }
    }
}
