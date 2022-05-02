using MyCompanyName.Identity;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MyCompanyName.Erp
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddApplicationModule),
        typeof(ErpDomainModule),
        typeof(ErpApplicationContractsModule),
        typeof(IdentityApplicationModule)
        )]
    public class ErpApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ErpApplicationModule>();
            });
        }
    }
}
