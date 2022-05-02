using MyCompanyName.Erp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyNameErp.Controllers
{
    public abstract class ErpController : AbpController
    {
        protected ErpController()
        {
            LocalizationResource = typeof(ErpResource);
        }
    }
}