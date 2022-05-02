using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MyCompanyName.TenantManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(TenantManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class TenantManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<TenantManagementDbContext>(options =>
            {
                options.AddDefaultRepositories<ITenantManagementDbContext>();
            });
        }
    }
}