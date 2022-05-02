using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MyCompanyName.Web.Shared.Pages;

namespace MyCompanyName.Efx.Web.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public void OnGet()
        {

        }
    }
}
