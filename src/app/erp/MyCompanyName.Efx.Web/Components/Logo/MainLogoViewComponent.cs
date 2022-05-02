using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyName.Efx.Web.Components
{
    public class MainLogoViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Components/Logo/Default.cshtml");
        }
    }
}
