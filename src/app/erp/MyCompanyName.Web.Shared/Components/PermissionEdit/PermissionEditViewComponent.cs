using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyName.Web.Shared.Components
{
    public class PermissionEditViewComponent : AbpViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("~/Components/PermissionEdit/Default.cshtml");
        }
    }
}
