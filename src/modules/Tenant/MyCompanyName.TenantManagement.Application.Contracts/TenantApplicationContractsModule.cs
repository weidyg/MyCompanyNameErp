using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MyCompanyName.TenantManagement
{
    [DependsOn(
        typeof(TenantManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class TenantApplicationContractsModule : AbpModule
    {

    }
}
