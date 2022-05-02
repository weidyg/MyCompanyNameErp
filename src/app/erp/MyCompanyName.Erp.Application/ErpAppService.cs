using MyCompanyName.Erp.Localization;
using Volo.Abp.Application.Services;

namespace MyCompanyName.Erp
{
    public abstract class ErpAppService : ApplicationService
    {
        protected ErpAppService()
        {
            LocalizationResource = typeof(ErpResource);
        }
    }
}
