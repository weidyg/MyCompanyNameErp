using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MyCompanyName.Web.Shared.Pages;
using Volo.Abp;

namespace MyCompanyName.Erp.Web.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public void OnGet()
        {
            if (!CurrentTenant.IsAvailable)
            {
                throw new UserFriendlyException(L["LoginSystemIsNotAllowed"]);
            }
        }
    }
}
