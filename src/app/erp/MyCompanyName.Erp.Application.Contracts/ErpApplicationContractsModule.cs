using MyCompanyName.Identity;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace MyCompanyName.Erp
{
    [DependsOn(
        typeof(ErpDomainSharedModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpObjectExtendingModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(IdentityApplicationContractsModule)
    )]
    public class ErpApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ErpDtoExtensions.Configure();
        }
    }
}
