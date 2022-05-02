using MyCompanyName.TenantManagement.Localization;
using Volo.Abp.Application.Services;

namespace MyCompanyName.TenantManagement
{
    public abstract class TenantAppService : ApplicationService
    {
        protected TenantAppService()
        {
            LocalizationResource = typeof(TenantManagementResource);
            ObjectMapperContext = typeof(TenantApplicationModule);
        }
    }
}
